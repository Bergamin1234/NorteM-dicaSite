using Microsoft.EntityFrameworkCore;
using Nortemedica.Domain.Aggregates.ProductAggregate;
using Nortemedica.Domain.RepositoryInterfaces;

namespace Nortemedica.Infrastructure.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetBySkuAsync(string sku)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Sku == sku);
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
    }
}