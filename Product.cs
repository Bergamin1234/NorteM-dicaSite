namespace Nortemedica.Domain.Aggregates.ProductAggregate;

public class Product
{
    public Guid Id { get; private set; }
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Ean { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }

    // Outras propriedades como Ncm, AnvisaRegistration, etc.
    // Construtor, métodos de domínio e validações...
}