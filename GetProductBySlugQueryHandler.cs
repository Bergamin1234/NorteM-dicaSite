using MediatR;
using Microsoft.EntityFrameworkCore;
using NorteMedicaSite.Application.Common.Interfaces;

namespace NorteMedicaSite.Application.Products.Queries;

public class GetProductBySlugQueryHandler : IRequestHandler<GetProductBySlugQuery, ProductDetailDto?>
{
    private readonly IApplicationDbContext _context;

    public GetProductBySlugQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProductDetailDto?> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
    {
        // Busca o produto no banco de dados pelo slug e projeta para o DTO
        var productDto = await _context.Products
            .AsNoTracking()
            .Where(p => p.Slug == request.Slug)
            .Select(p => new ProductDetailDto
            {
                Id = p.Id.ToString(),
                Sku = p.Sku,
                Name = p.Name,
                Slug = p.Slug,
                Description = p.Description
            })
            .FirstOrDefaultAsync(cancellationToken);

        return productDto;
    }
}