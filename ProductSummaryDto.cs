namespace NorteMedicaSite.Application.Products.Queries;

/// <summary>
/// DTO para o resumo de um produto, usado em listagens.
/// </summary>
public class ProductSummaryDto
{
    public string Id { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}