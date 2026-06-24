using Microsoft.Extensions.Logging;
using Nortemedica.Application.Features.Alligator.Dtos;
using Nortemedica.Application.Features.Alligator.Interfaces;
using Nortemedica.Domain.Aggregates.CustomerAggregate;
using Nortemedica.Domain.Aggregates.ProductAggregate;
using Nortemedica.Domain.RepositoryInterfaces;

namespace Nortemedica.Infrastructure.Integration.Alligator;

/// <summary>
/// Serviço responsável por orquestrar a sincronização de dados entre o ERP Alligator e a base de dados local.
/// </summary>
public class AlligatorSynchronizationService : IAlligatorSynchronizationService
{
    private readonly ILogger<AlligatorSynchronizationService> _logger;
    private readonly IAlligatorApiClient _apiClient;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AlligatorSynchronizationService(
        ILogger<AlligatorSynchronizationService> logger,
        IAlligatorApiClient apiClient,
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _apiClient = apiClient;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> SynchronizeProductBySkuAsync(string sku, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando sincronização do produto com SKU: {Sku}", sku);

        var productDto = await _apiClient.GetProductBySkuAsync(sku, cancellationToken);
        if (productDto == null)
        {
            _logger.LogWarning("Produto com SKU {Sku} não encontrado no ERP Alligator.", sku);
            return false; // Ou tratar como exclusão, dependendo da regra de negócio
        }

        var product = await _productRepository.GetBySkuAsync(sku);
        if (product == null)
        {
            _logger.LogInformation("Produto com SKU {Sku} não existe localmente. Criando novo produto.", sku);
            product = new Product { Sku = sku };
            _productRepository.Add(product);
        }

        // Mapeamento do DTO para a Entidade (Em um projeto real, usar AutoMapper)
        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.Price = productDto.Price;
        product.Ean = productDto.Ean;

        // Lógica para buscar ou criar a categoria
        var category = await _categoryRepository.GetByNameAsync(productDto.Category);
        if (category == null)
        {
            // A criação de categoria pode ser mais complexa (ex: precisa de aprovação)
            _logger.LogWarning("Categoria '{CategoryName}' não encontrada para o produto {Sku}. O produto ficará sem categoria.", productDto.Category, sku);
        }
        product.Category = category;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Produto com SKU {Sku} sincronizado com sucesso.", sku);
        return true;
    }

    public async Task<bool> SynchronizeCustomerByExternalIdAsync(string externalId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando sincronização do cliente com ID Externo: {ExternalId}", externalId);

        // Supondo que o ApiClient tenha um método para buscar clientes
        var customerDto = await _apiClient.GetCustomerByExternalIdAsync(externalId, cancellationToken);
        if (customerDto == null)
        {
            _logger.LogWarning("Cliente com ID Externo {ExternalId} não encontrado no ERP Alligator.", externalId);
            return false;
        }

        var customer = await _customerRepository.GetByExternalIdAsync(externalId);
        if (customer == null)
        {
            _logger.LogInformation("Cliente com ID Externo {ExternalId} não existe localmente. Criando novo cliente.", externalId);
            customer = new Customer { ExternalId = externalId };
            _customerRepository.Add(customer);
        }

        // Mapeamento do DTO para a Entidade
        customer.Name = customerDto.Name;
        customer.Document = customerDto.Document;
        customer.Email = customerDto.Email;
        customer.PhoneNumber = customerDto.PhoneNumber;
        customer.IsActive = customerDto.IsActive;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Cliente com ID Externo {ExternalId} sincronizado com sucesso.", externalId);
        return true;
    }
}