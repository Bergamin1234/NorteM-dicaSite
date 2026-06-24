using MediatR;
using System.Collections.Generic;

namespace Nortemedica.Application.Features.Products.Queries.GetProducts;

public record GetProductsQuery(string? SearchTerm) : IRequest<IEnumerable<ProductSummaryDto>>;