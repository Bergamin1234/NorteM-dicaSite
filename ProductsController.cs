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
    public async Task<IActionResult> GetProducts([FromQuery] string? searchTerm)
    {
        var query = new GetProductsQuery(searchTerm);
        var result = await _mediator.Send(query);

        return Ok(result);
    }
}