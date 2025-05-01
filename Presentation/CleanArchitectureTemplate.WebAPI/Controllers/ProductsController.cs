using CleanArchitectureTemplate.Application.Features.Products.Commands.CreateProduct;
using CleanArchitectureTemplate.Application.Features.Products.Commands.DeleteProduct;
using CleanArchitectureTemplate.Application.Features.Products.Commands.UpdateProduct;
using CleanArchitectureTemplate.Application.Features.Products.Queries.GetAllProducts;
using CleanArchitectureTemplate.Application.Features.Products.Queries.GetProductById;
using CleanArchitectureTemplate.Application.RequestParameters;
using CleanArchitectureTemplate.Domain.Constants.StringConstants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ResponseCache(Duration = 360, Location = ResponseCacheLocation.Client, VaryByQueryKeys = ["*"])]
    public async Task<IActionResult> GetAll(
        [FromQuery] Pagination pagination,
        [FromQuery] bool includeCategories,
        [FromQuery] bool includeProductImageFiles)
    {
        var request = new GetAllProductsQueryRequest(pagination, includeCategories, includeProductImageFiles);
        return await _mediator.Send(request);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, [FromQuery] bool includeCategories, [FromQuery] bool includeProductImageFiles)
    {
        var request = new GetProductByIdQueryRequest(id, includeCategories, includeProductImageFiles);
        return await _mediator.Send(request);
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Store)]
    public async Task<IActionResult> Create([FromBody] CreateProductCommandRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPut]
    [Authorize(Roles = UserRoles.StoreOrAdmin)]
    public async Task<IActionResult> Update([FromBody] UpdateProductCommandRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = UserRoles.StoreOrAdmin)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var request = new DeleteProductCommandRequest(id);
        return await _mediator.Send(request);
    }
}