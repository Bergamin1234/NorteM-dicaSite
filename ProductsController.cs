using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nortemedica.Application.Features.Products.Queries.GetProductBySlug;
using Nortemedica.Application.Features.Products.Queries.GetProducts;
using Nortemedica.Application.Features.Products.Commands.SynchronizeProductBySku;
using System.Threading.Tasks;

namespace Nortemedica.API.Controllers;

[ApiController]
[Route("api/v1/products")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Aciona a sincronização manual de um produto a partir do ERP Alligator, baseado no SKU.
    /// </summary>
    /// <param name="sku">O SKU do produto a ser sincronizado.</param>
    /// <returns>Status da operação de sincronização.</returns>
    [HttpPost("sync/{sku}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SynchronizeProduct(string sku)
    {
        var command = new SynchronizeProductBySkuCommand(sku);
        var result = await _mediator.Send(command);

        return result ? Ok($"Sincronização do produto SKU '{sku}' concluída.") : NotFound($"Produto SKU '{sku}' não encontrado no ERP para sincronização.");
    }

    /// <summary>
    /// Busca os detalhes de um produto pelo seu slug.
    /// </summary>
    /// <param name="slug">O slug (URL amigável) do produto.</param>
    /// <returns>Os detalhes do produto.</returns>
    [HttpGet("{slug}")]
    [ProducesResponseType(typeof(ProductDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductBySlug(string slug)
    {
        var query = new GetProductBySlugQuery(slug);
        var result = await _mediator.Send(query);

        return result != null ? Ok(result) : NotFound();
    }

    /// <summary>
    /// Busca uma lista de todos os produtos.
    /// </summary>
    /// <returns>Uma lista de produtos.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductSummaryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts()
    {
        var query = new GetProductsQuery();
        var result = await _mediator.Send(query);

        return Ok(result);
    }
}