namespace NorteMedicaSite.Domain.Entities;

public enum DocumentType
{
    Licitacao,
    AtaRegistroPreco
}

public class Document
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime PublicationDate { get; set; }
    public DocumentType Type { get; set; }
    public string FilePath { get; set; } = string.Empty; // Caminho para o arquivo PDF no servidor/storage
}