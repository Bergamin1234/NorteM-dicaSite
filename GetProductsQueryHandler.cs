using MediatR;

namespace NorteMedicaSite.Application.Products.Queries;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductSummaryDto>>
{
    public Task<IEnumerable<ProductSummaryDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        // --- LÓGICA SIMULADA (MOCK) ---
        // Usamos uma lista de produtos resumidos para simular o banco de dados.
        var allProducts = new List<ProductSummaryDto>
        {
            new() { Id = "prod_1", Slug = "luva-cirurgica-pro", Name = "Luva Cirúrgica Pro" },
            new() { Id = "prod_2", Slug = "mascara-n95-plus", Name = "Máscara N95 Plus" },
            new() { Id = "prod_3", Slug = "seringa-descartavel-10ml", Name = "Seringa Descartável 10ml" }
        };

        // Se um termo de busca foi fornecido, filtramos a lista.
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTermLower = request.SearchTerm.ToLowerInvariant();
            var filteredProducts = allProducts
                .Where(p => p.Name.ToLowerInvariant().Contains(searchTermLower));
            
            return Task.FromResult(filteredProducts);
        }

        return Task.FromResult<IEnumerable<ProductSummaryDto>>(allProducts);
    }
}