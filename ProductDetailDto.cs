namespace Nortemedica.Application.Features.Products.Queries.GetProductBySlug;

public class ProductDetailDto
{
    public Guid Id { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? CategoryName { get; set; }
}