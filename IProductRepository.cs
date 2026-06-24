using Nortemedica.Domain.Aggregates.ProductAggregate;

namespace Nortemedica.Domain.RepositoryInterfaces;

public interface IProductRepository
{
    Task<Product?> GetBySkuAsync(string sku);
    Task<Product?> GetBySlugAsync(string slug);
    void Add(Product product);
}