using MediatR;
using NorteMedicaSite.Application.Common.Models;

namespace NorteMedicaSite.Application.Products.Queries;

public record GetProductsQuery(string? SearchTerm, int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedList<ProductSummaryDto>>;