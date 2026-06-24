using MediatR;

namespace NorteMedicaSite.Application.Products.Queries;

public record GetProductsQuery(string? SearchTerm) : IRequest<IEnumerable<ProductSummaryDto>>;