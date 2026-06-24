using MediatR;

namespace Nortemedica.Application.Features.Products.Commands.SynchronizeProductBySku;

// Usamos 'record' para um DTO imutável e conciso.
// IRequest<bool> indica que este comando retorna um booleano ao ser executado.
public record SynchronizeProductBySkuCommand(string Sku) : IRequest<bool>;