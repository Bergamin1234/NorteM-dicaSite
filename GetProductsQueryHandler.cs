using MediatR;
using NorteMedicaSite.Application.Common.Models;

using MediatR;
using Microsoft.EntityFrameworkCore;
using NorteMedicaSite.Application.Common.Interfaces;
using NorteMedicaSite.Application.Common.Models;

namespace NorteMedicaSite.Application.Products.Queries;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PaginatedList<ProductSummaryDto>>
{
    private readonly IApplicationDbContext _context;

    public GetProductsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<ProductSummaryDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        // Começamos com uma consulta IQueryable, que permite construir a query passo a passo
        var queryableProducts = _context.Products.AsNoTracking();

        // Se um termo de busca foi fornecido, filtramos a lista.
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            // Usando Full-Text Search para uma busca textual eficiente e performática.
            // Isso é muito mais rápido do que p.Name.Contains() em grandes volumes de dados.
            // Requer que o Full-Text Search esteja configurado no SQL Server para a coluna 'Name'.
            queryableProducts = queryableProducts.Where(p => EF.Functions.FreeText(p.Name, request.SearchTerm));
        }

        // Executa a contagem total de itens (respeitando o filtro)
        var count = await queryableProducts.CountAsync(cancellationToken);

        // Aplica a paginação e projeta o resultado para o DTO
        var items = await queryableProducts
            .OrderBy(p => p.Name) // É uma boa prática ter uma ordenação padrão
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(p => new ProductSummaryDto
            {
                Id = p.Id.ToString(),
                Name = p.Name,
                Slug = p.Slug
            })
            .ToListAsync(cancellationToken);

        return new PaginatedList<ProductSummaryDto>(items, count, request.PageNumber, request.PageSize);
    }
}