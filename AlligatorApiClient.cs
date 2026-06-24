using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nortemedica.Application.Features.Alligator.Dtos;
using Nortemedica.Application.Features.Alligator.Interfaces;

namespace Nortemedica.Infrastructure.Integration.Alligator;

/// <summary>
/// Cliente HTTP para se comunicar com a API do ERP Alligator.
/// Esta classe encapsula a lógica de requisições e respostas.
/// </summary>
public class AlligatorApiClient : IAlligatorApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AlligatorApiClient> _logger;

    // HttpClient é injetado via IHttpClientFactory para gerenciamento otimizado de sockets.
    public AlligatorApiClient(HttpClient httpClient, ILogger<AlligatorApiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        // A BaseAddress e Headers de autenticação seriam configurados na inicialização do serviço.
    }

    public async Task<AlligatorProductSyncDto?> GetProductBySkuAsync(string sku, CancellationToken cancellationToken)
    {
        try
        {
            // Exemplo de chamada à API do Alligator para buscar um produto.
            return await _httpClient.GetFromJsonAsync<AlligatorProductSyncDto>($"/api/products/{sku}", cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Erro ao buscar produto com SKU {Sku} na API do Alligator.", sku);
            return null;
        }
    }
}