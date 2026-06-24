namespace Nortemedica.Domain.Aggregates.ProductAggregate;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;

    // Relacionamento com produtos
}