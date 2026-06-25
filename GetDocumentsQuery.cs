using MediatR;
using NorteMedicaSite.Application.Common.Models;
using NorteMedicaSite.Domain.Entities;

namespace NorteMedicaSite.Application.Documents.Queries;

public record GetDocumentsQuery(
    DocumentType Type,
    string? SearchTerm,
    int PageNumber = 1,
    int PageSize = 15) : IRequest<PaginatedList<DocumentDto>>;