using MediatR;
using Nortemedica.Domain.RepositoryInterfaces;

namespace Nortemedica.Application.Features.Products.Queries.GetProductBySlug;

public class GetProductBySlugQueryHandler : IRequestHandler<GetProductBySlugQuery, ProductDetailDto?>
{
    private readonly IProductRepository _productRepository;

    public GetProductBySlugQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDetailDto?> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetBySlugAsync(request.Slug);

        if (product == null)
        {
            return null;
        }

        return new ProductDetailDto
        {
            Id = product.Id,
            Sku = product.Sku,
            Slug = product.Slug,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryName = product.Category?.Name
        };
    }
}