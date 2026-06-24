using Microsoft.EntityFrameworkCore;
using Nortemedica.Domain.Aggregates.CustomerAggregate;
using Nortemedica.Domain.RepositoryInterfaces;

namespace Nortemedica.Infrastructure.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Customer?> GetByExternalIdAsync(string externalId)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.ExternalId == externalId);
    }

    public void Add(Customer customer)
    {
        _context.Customers.Add(customer);
    }
}