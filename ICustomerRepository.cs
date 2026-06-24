using Nortemedica.Domain.Aggregates.CustomerAggregate;

namespace Nortemedica.Domain.RepositoryInterfaces;

public interface ICustomerRepository
{
    Task<Customer?> GetByExternalIdAsync(string externalId);
    void Add(Customer customer);
}