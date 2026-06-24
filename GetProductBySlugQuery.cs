using MediatR;

namespace Nortemedica.Application.Features.Products.Queries.GetProductBySlug;

public record GetProductBySlugQuery(string Slug) : IRequest<ProductDetailDto?>;