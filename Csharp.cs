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
            // Loga o erro crítico. A mensagem não será reenfileirada para evitar loops de "poison message".
            // Se uma DLQ (Dead Letter Queue) estiver configurada, a mensagem será enviada para lá.
            _logger.LogError(ex, "Erro crítico ao processar evento de sincronização do Alligator. Enviando para DLQ (ou descartando).");
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