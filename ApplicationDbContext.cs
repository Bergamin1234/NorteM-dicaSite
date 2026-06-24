using Microsoft.EntityFrameworkCore;
using Nortemedica.Domain.Aggregates.CustomerAggregate;
using Nortemedica.Domain.Aggregates.ProductAggregate;
using Nortemedica.Domain.RepositoryInterfaces;

namespace Nortemedica.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(b =>
        {
            b.HasKey(p => p.Id);
            b.HasIndex(p => p.Sku).IsUnique();
            b.HasIndex(p => p.Slug).IsUnique(); // Adicionar índice para o Slug
            b.Property(p => p.Name).IsRequired().HasMaxLength(255);
            b.Property(p => p.Price).HasColumnType("decimal(18,4)");
        });

        modelBuilder.Entity<Customer>(b =>
        {
            b.HasKey(c => c.Id);
            b.HasIndex(c => c.ExternalId).IsUnique();
            b.HasIndex(c => c.Document).IsUnique();
            b.Property(c => c.Name).IsRequired().HasMaxLength(255);
            b.Property(c => c.Email).IsRequired().HasMaxLength(255);
        });
    }
}