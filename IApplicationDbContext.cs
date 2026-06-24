using Microsoft.EntityFrameworkCore;
using NorteMedicaSite.Domain.Entities;

namespace NorteMedicaSite.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Brand> Brands { get; }
    DbSet<Document> Documents { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}