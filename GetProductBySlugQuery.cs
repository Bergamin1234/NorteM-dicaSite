using MediatR;

namespace NorteMedicaSite.Application.Products.Queries;

// IRequest<ProductDetailDto?> indica que esta query, quando executada,
// retornará um ProductDetailDto ou null.
public record GetProductBySlugQuery(string Slug) : IRequest<ProductDetailDto?>;