namespace Nortemedica.Domain.Aggregates.CustomerAggregate;

public class Customer
{
    public Guid Id { get; private set; }
    public string ExternalId { get; set; } = string.Empty; // ID do cliente no ERP
    public string Name { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; }

    // Construtor, métodos de domínio e validações...
}