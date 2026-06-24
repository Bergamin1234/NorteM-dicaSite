using MediatR;
using Nortemedica.Domain.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nortemedica.Application.Features.Products.Queries.GetProducts;
namespace NorteMedicaSite.Application.Products.Queries;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductSummaryDto>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsQueryHandler(IProductRepository productRepository)
    public Task<IEnumerable<ProductSummaryDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        _productRepository = productRepository;
    }
        // --- LÓGICA SIMULADA (MOCK) ---
        // Usamos uma lista de produtos resumidos para simular o banco de dados.
        var allProducts = new List<ProductSummaryDto>
        {
            new() { Id = "prod_1", Slug = "luva-cirurgica-pro", Name = "Luva Cirúrgica Pro" },
            new() { Id = "prod_2", Slug = "mascara-n95-plus", Name = "Máscara N95 Plus" },
            new() { Id = "prod_3", Slug = "seringa-descartavel-10ml", Name = "Seringa Descartável 10ml" }
        };

    public async Task<IEnumerable<ProductSummaryDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(request.SearchTerm);
        // Se um termo de busca foi fornecido, filtramos a lista.
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTermLower = request.SearchTerm.ToLowerInvariant();
            var filteredProducts = allProducts
                .Where(p => p.Name.ToLowerInvariant().Contains(searchTermLower));
            
            return Task.FromResult(filteredProducts);
        }

        return products.Select(p => new ProductSummaryDto
        {
            Id = p.Id,
            Name = p.Name,
            Slug = p.Slug,
            Price = p.Price
        });
        return Task.FromResult<IEnumerable<ProductSummaryDto>>(allProducts);
    }
}