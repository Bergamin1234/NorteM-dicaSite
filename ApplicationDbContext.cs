using Microsoft.EntityFrameworkCore;
using Nortemedica.Domain.Aggregates.CustomerAggregate;
using Nortemedica.Domain.Aggregates.ProductAggregate;
using Nortemedica.Domain.RepositoryInterfaces;
using NorteMedicaSite.Domain.Entities;
using System.Reflection;

namespace Nortemedica.Infrastructure.Data;
namespace NorteMedicaSite.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IUnitOfWork
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Document> Documents => Set<Document>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(modelBuilder);
        // Aplica todas as configurações de entidade que estiverem no mesmo assembly
        // (bom para organizar configurações de relacionamentos complexos em arquivos separados)
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

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
        base.OnModelCreating(builder);
    }
}