using CommercePortal.Application.Features.Products.Commands.CreateProduct;
using CommercePortal.Application.Features.Products.Commands.DeleteProduct;
using CommercePortal.Application.Features.Products.Commands.UpdateProduct;
using CommercePortal.Application.Features.Products.Queries.GetAllProducts;
using CommercePortal.Application.Features.Products.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommercePortal.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllProductsQueryRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, [FromQuery] bool includeCategories, [FromQuery] bool includeOrders, [FromQuery] bool includeProductImageFiles)
    {
        var request = new GetProductByIdQueryRequest(id, includeCategories, includeOrders, includeProductImageFiles);
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProductCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var request = new DeleteProductCommandRequest(id);
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}