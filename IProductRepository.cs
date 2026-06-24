using Nortemedica.Domain.Aggregates.ProductAggregate;

namespace Nortemedica.Domain.RepositoryInterfaces;

public interface IProductRepository
{
    Task<Product?> GetBySkuAsync(string sku);
    void Add(Product product);
}