        IServiceProvider serviceProvider,
        IOptions<RabbitMQOptions> rabbitMQOptions)
    {
        // O nome do arquivo "Csharp.cs" é muito genérico. Considere renomeá-lo para algo descritivo,
        // como "AlligatorSyncConsumerService.cs", para refletir sua responsabilidade.
        _logger = logger;
        _serviceProvider = serviceProvider;
        _rabbitMQOptions = rabbitMQOptions.Value;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        await InitializeRabbitMQAsync(cancellationToken);
        await base.StartAsync(cancellationToken);
    }

    private async Task InitializeRabbitMQAsync(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory { HostName = _rabbitMQOptions.HostName, DispatchConsumersAsync = true };

        // Lógica de retry para resiliência na conexão
        // Em um cenário de produção, considere usar uma biblioteca como Polly
        for (int i = 0; i < 5; i++)
        {
            try
            {
                _connection = await factory.CreateConnectionAsync(cancellationToken);
                _channel = await _connection.CreateChannelAsync(cancellationToken);
                break; // Conexão bem-sucedida
            }
            catch (Exception ex)
            {
...
            var alligatorService = scope.ServiceProvider.GetRequiredService<IAlligatorService>();

            // Exemplo de como lidar com múltiplos tipos de eventos no futuro
            // var baseEvent = JsonSerializer.Deserialize<BaseEvent>(message);
            // switch(baseEvent.EventType) { ... }

            var syncEvent = JsonSerializer.Deserialize<InventorySyncEvent>(message);
            if (syncEvent?.Sku != null)
            {
                // Utilizar um CancellationToken vinculado ao token de parada do serviço.
                // Isso garante que operações longas possam ser canceladas se o serviço for interrompido.
                bool success = await alligatorService.SynchronizeInventoryAsync(syncEvent.Sku, _stoppingCts.Token);
                if (success)
                {
                    await _channel!.BasicAckAsync(ea.DeliveryTag, multiple: false);
                    return;
                }
            }

            // Se a lógica de negócio falhar ou a mensagem for inválida, rejeita e envia para a DLQ
            _logger.LogWarning("Falha na lógica de negócio ou mensagem inválida. Enviando para DLQ. Mensagem: {Message}", message);
            await _channel!.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: false);
        }
        catch (JsonException jsonEx)
        {
            _logger.LogError(jsonEx, "Erro de deserialização da mensagem. Enviando para DLQ. Mensagem: {Message}", message);
            await _channel!.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro crítico ao processar evento de sincronização do Alligator. A mensagem será reenfileirada para nova tentativa.");
            // Para erros transitórios (ex: falha de rede com o DB), reenfileirar pode ser uma opção.
            // Cuidado para não criar um loop de envenenamento (poison message).
            await _channel!.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: false); // Mudei para false para evitar poison messages em caso de erro de código.
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_channel != null) await _channel.CloseAsync();
        if (_connection != null) await _connection.CloseAsync();
        _stoppingCts.Cancel(); // Sinaliza o cancelamento para as operações em andamento
        await base.StopAsync(cancellationToken);
    }
}

// Definição do evento de sincronização
public class InventorySyncEvent
{
    public string Sku { get; set; } = string.Empty;
}
