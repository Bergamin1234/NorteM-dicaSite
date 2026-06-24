namespace NorteMedicaSite.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }

    public Guid BrandId { get; set; }
    public Brand Brand { get; set; } = null!;
    // Futuramente, adicionaremos a Categoria aqui também.
}