using Microsoft.EntityFrameworkCore;
using Nortemedica.Domain.Aggregates.ProductAggregate;
using Nortemedica.Domain.RepositoryInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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

    public async Task<Product?> GetBySlugAsync(string slug)
    {
        return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Slug == slug);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
    }
}