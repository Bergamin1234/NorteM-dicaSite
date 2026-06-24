using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nortemedica.Application.Features.Alligator.Interfaces;

namespace Nortemedica.API.Controllers;

/// <summary>
/// Endpoint para receber notificações (webhooks) em tempo real do ERP Alligator.
/// </summary>
[ApiController]
[Route("api/v1/webhooks/alligator")]
public class AlligatorWebhookController : ControllerBase
{
    private readonly ILogger<AlligatorWebhookController> _logger;
    private readonly IAlligatorService _alligatorService;

    public AlligatorWebhookController(ILogger<AlligatorWebhookController> logger, IAlligatorService alligatorService)
    {
        _logger = logger;
        _alligatorService = alligatorService;
    }

    [HttpPost]
    public async Task<IActionResult> ReceiveNotification()
    {
        using var reader = new StreamReader(Request.Body, Encoding.UTF8);
        var rawPayload = await reader.ReadToEndAsync();
        var eventType = Request.Headers["X-Alligator-Event"].ToString(); // Exemplo de header

        _logger.LogInformation("Webhook do Alligator recebido. Tipo: {EventType}", eventType);

        var success = await _alligatorService.ProcessWebhookNotificationAsync(eventType, rawPayload);

        return success ? Ok() : BadRequest("Falha ao processar o webhook.");
    }
}