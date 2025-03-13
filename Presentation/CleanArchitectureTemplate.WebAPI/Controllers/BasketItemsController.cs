using CleanArchitectureTemplate.Application.Features.BasketItems.Commands.AddBasketItemToBasket;
using CleanArchitectureTemplate.Application.Features.BasketItems.Commands.RemoveBasketItemFromBasket;
using CleanArchitectureTemplate.Application.Features.BasketItems.Queries.GetBasketItemById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
public class BasketItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BasketItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBasketItemById([FromRoute] Guid id)
    {
        var request = new GetBasketItemByIdQueryRequest(id);
        return await _mediator.Send(request);
    }

    [HttpPost]
    public async Task<IActionResult> AddBasketItemToBasket([FromBody] AddBasketItemToBasketCommandRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveBasketItemFromBasket([FromBody] RemoveBasketItemFromBasketCommandRequest request)
    {
        return await _mediator.Send(request);
    }
}