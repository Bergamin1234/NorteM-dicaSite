using MediatR;
using Microsoft.EntityFrameworkCore;
using NorteMedicaSite.Application.Common.Interfaces;
using NorteMedicaSite.Application.Common.Models;

namespace NorteMedicaSite.Application.Documents.Queries;

public class GetDocumentsQueryHandler : IRequestHandler<GetDocumentsQuery, PaginatedList<DocumentDto>>
{
    private readonly IApplicationDbContext _context;

    public GetDocumentsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<DocumentDto>> Handle(GetDocumentsQuery request, CancellationToken cancellationToken)
    {
        var queryableDocuments = _context.Documents
            .AsNoTracking()
            .Where(d => d.Type == request.Type);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            queryableDocuments = queryableDocuments.Where(d => d.Title.Contains(request.SearchTerm));
        }

        var count = await queryableDocuments.CountAsync(cancellationToken);

        var items = await queryableDocuments
            .OrderByDescending(d => d.PublicationDate) // Ordena pelos mais recentes primeiro
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(d => new DocumentDto
            {
                Id = d.Id.ToString(),
                Title = d.Title,
                PublicationDate = d.PublicationDate.ToString("dd/MM/yyyy"), // Formata a data
                FilePath = d.FilePath
            })
            .ToListAsync(cancellationToken);

        return new PaginatedList<DocumentDto>(items, count, request.PageNumber, request.PageSize);
    }
}