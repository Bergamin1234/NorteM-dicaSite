namespace NorteMedicaSite.Application.Documents.Queries;

/// <summary>
/// DTO para os dados de um documento (Licitação ou Ata).
/// </summary>
public class DocumentDto
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string PublicationDate { get; set; } = string.Empty; // Data formatada como string
    public string FilePath { get; set; } = string.Empty;
}