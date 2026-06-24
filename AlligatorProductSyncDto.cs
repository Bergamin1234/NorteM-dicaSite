namespace Nortemedica.Application.Features.Alligator.Dtos;

/// <summary>
/// Data Transfer Object para sincronização de produtos vindo do ERP Alligator.
/// </summary>
public class AlligatorProductSyncDto
{
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Ean { get; set; } = string.Empty;
}