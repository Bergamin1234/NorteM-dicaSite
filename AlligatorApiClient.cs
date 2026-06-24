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
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Produto com SKU {Sku} não encontrado na API do Alligator.", sku);
            return null;
        }
        // Para outros erros de HTTP, a política do Polly (configurada no Program.cs) fará o retry.
        // Se todas as tentativas falharem, a exceção será lançada e deverá ser tratada pela camada de serviço que chamou este método.
    }
