using Nortemedica.Domain.Aggregates.ProductAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nortemedica.Domain.RepositoryInterfaces;

public interface IProductRepository
{
    Task<Product?> GetBySkuAsync(string sku);
    Task<Product?> GetBySlugAsync(string slug);
    Task<IEnumerable<Product>> GetAllAsync(string? searchTerm = null);
    void Add(Product product);
}