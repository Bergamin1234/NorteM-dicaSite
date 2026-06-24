namespace Nortemedica.Application.Features.Alligator.Dtos;

/// <summary>
/// DTO para os dados de clientes vindos do ERP Alligator.
/// </summary>
public class AlligatorCustomerSyncDto
{
    public string ExternalId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty; // CNPJ ou CPF
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}