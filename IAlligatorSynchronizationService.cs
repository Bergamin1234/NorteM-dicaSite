namespace Nortemedica.Application.Features.Alligator.Interfaces;

public interface IAlligatorSynchronizationService
{
    Task<bool> SynchronizeProductBySkuAsync(string sku, CancellationToken cancellationToken);
    Task<bool> SynchronizeCustomerByExternalIdAsync(string externalId, CancellationToken cancellationToken);
    // Outros métodos de sincronização (pedidos, estoque, etc.)
}