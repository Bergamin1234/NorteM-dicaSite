using MediatR;
using NorteMedicaSite.Application.Common.Models;

namespace NorteMedicaSite.Application.Products.Queries;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PaginatedList<ProductSummaryDto>>
{
    public Task<PaginatedList<ProductSummaryDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        // --- LÓGICA SIMULADA (MOCK) ---
        // Usamos uma lista de produtos resumidos para simular o banco de dados.
        var mockProducts = new List<ProductSummaryDto>
        {
            new() { Id = "prod_1", Slug = "luva-cirurgica-pro", Name = "Luva Cirúrgica Pro" },
            new() { Id = "prod_2", Slug = "mascara-n95-plus", Name = "Máscara N95 Plus" },
            new() { Id = "prod_3", Slug = "seringa-descartavel-10ml", Name = "Seringa Descartável 10ml" },
            new() { Id = "prod_4", Slug = "avental-descartavel", Name = "Avental Descartável" },
            new() { Id = "prod_5", Slug = "termometro-digital", Name = "Termômetro Digital Infravermelho" },
            new() { Id = "prod_6", Slug = "oximetro-de-pulso", Name = "Oxímetro de Pulso de Dedo" },
            new() { Id = "prod_7", Slug = "gaze-esterilizada", Name = "Compressa de Gaze Esterilizada" },
            new() { Id = "prod_8", Slug = "atadura-elastica", Name = "Atadura Elástica" },
            new() { Id = "prod_9", Slug = "alcool-em-gel-70", Name = "Álcool em Gel 70% 500ml" },
            new() { Id = "prod_10", Slug = "kit-primeiros-socorros", Name = "Kit de Primeiros Socorros Básico" },
            new() { Id = "prod_11", Slug = "esparadrapo-impermeavel", Name = "Esparadrapo Impermeável" }
        };

        var queryableProducts = mockProducts.AsQueryable();

        // Se um termo de busca foi fornecido, filtramos a lista.
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            queryableProducts = queryableProducts.Where(p => p.Name.ToLowerInvariant().Contains(request.SearchTerm.ToLowerInvariant()));
        }

        var count = queryableProducts.Count();
        var items = queryableProducts.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

        var paginatedList = new PaginatedList<ProductSummaryDto>(items, count, request.PageNumber, request.PageSize);

        return Task.FromResult(paginatedList);
    }
}