namespace NorteMedicaSite.Application.Products.Queries;

/// <summary>
/// DTO (Data Transfer Object) para os detalhes de um produto.
/// </summary>
public class ProductDetailDto
{
    public string Id { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}