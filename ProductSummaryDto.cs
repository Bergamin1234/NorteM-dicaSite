namespace Nortemedica.Application.Features.Products.Queries.GetProducts;

public class ProductSummaryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public decimal Price { get; set; }
    // public string? ImageUrl { get; set; } // A ser adicionado futuramente
}