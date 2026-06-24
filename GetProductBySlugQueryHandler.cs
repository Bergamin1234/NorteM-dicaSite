using MediatR;

namespace NorteMedicaSite.Application.Products.Queries;

public class GetProductBySlugQueryHandler : IRequestHandler<GetProductBySlugQuery, ProductDetailDto?>
{
    // Em uma aplicação real, você injetaria seu DbContext ou repositório aqui.
    // Ex: private readonly IApplicationDbContext _context;
    // public GetProductBySlugQueryHandler(IApplicationDbContext context) => _context = context;

    public Task<ProductDetailDto?> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
    {
        // --- LÓGICA SIMULADA (MOCK) ---
        // Aqui estamos criando uma lista de produtos na memória para simular um banco de dados.
        var mockProducts = new List<ProductDetailDto>
        {
            new() { Id = "prod_1", Sku = "NM-001", Slug = "luva-cirurgica-pro", Name = "Luva Cirúrgica Pro", Description = "Luva de alta qualidade para procedimentos." },
            new() { Id = "prod_2", Sku = "NM-002", Slug = "mascara-n95-plus", Name = "Máscara N95 Plus", Description = "Máscara com filtragem superior." },
            new() { Id = "prod_3", Sku = "NM-003", Slug = "seringa-descartavel-10ml", Name = "Seringa Descartável 10ml", Description = "Pacote com 100 unidades." }
        };

        // Procuramos o produto na nossa lista simulada usando o slug da requisição.
        var product = mockProducts.FirstOrDefault(p => p.Slug == request.Slug);

        // Se o produto não for encontrado, retornamos null, e o controller retornará um 404 Not Found.
        if (product == null)
        {
            return Task.FromResult<ProductDetailDto?>(null);
        }

        return Task.FromResult<ProductDetailDto?>(product);
    }
}