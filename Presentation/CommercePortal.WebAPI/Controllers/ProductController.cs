using CommercePortal.Application.Abstractions.Storage;
using CommercePortal.Application.Features.Commands.ProductImageFiles.UploadProductImage;
using CommercePortal.Application.Features.Commands.Products.CreateProduct;
using CommercePortal.Application.Features.Commands.Products.DeleteProduct;
using CommercePortal.Application.Features.Commands.Products.UpdateProduct;
using CommercePortal.Application.Features.Queries.Products.GetAllProducts;
using CommercePortal.Application.Features.Queries.Products.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CommercePortal.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
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
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var request = new GetProductByIdQueryRequest(id);
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

    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadImage([FromForm] UploadProductImageCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}