using Nortemedica.Domain.Aggregates.ProductAggregate;

namespace Nortemedica.Domain.RepositoryInterfaces;

public interface ICategoryRepository
{
    Task<Category?> GetByNameAsync(string name);
}