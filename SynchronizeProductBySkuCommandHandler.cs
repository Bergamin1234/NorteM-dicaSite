using MediatR;
using Microsoft.Extensions.Logging;
using Nortemedica.Application.Features.Alligator.Interfaces;

namespace Nortemedica.Application.Features.Products.Commands.SynchronizeProductBySku;

public class SynchronizeProductBySkuCommandHandler : IRequestHandler<SynchronizeProductBySkuCommand, bool>
{
    private readonly IAlligatorSynchronizationService _syncService;
    private readonly ILogger<SynchronizeProductBySkuCommandHandler> _logger;

    public SynchronizeProductBySkuCommandHandler(
        IAlligatorSynchronizationService syncService,
        ILogger<SynchronizeProductBySkuCommandHandler> logger)
    {
        _syncService = syncService;
        _logger = logger;
    }

    public async Task<bool> Handle(SynchronizeProductBySkuCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handler acionado para sincronizar produto com SKU: {Sku}", request.Sku);
        return await _syncService.SynchronizeProductBySkuAsync(request.Sku, cancellationToken);
    }
}