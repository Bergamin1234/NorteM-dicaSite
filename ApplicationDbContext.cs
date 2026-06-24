using Microsoft.EntityFrameworkCore;
using NorteMedicaSite.Domain.Entities;
using System.Reflection;

namespace NorteMedicaSite.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Document> Documents => Set<Document>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Aplica todas as configurações de entidade que estiverem no mesmo assembly
        // (bom para organizar configurações de relacionamentos complexos em arquivos separados)
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}