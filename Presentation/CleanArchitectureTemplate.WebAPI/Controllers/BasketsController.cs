using CleanArchitectureTemplate.Application.Features.Baskets.Commads.ClearBasket;
using CleanArchitectureTemplate.Application.Features.Baskets.Queries.GetAllBaskets;
using CleanArchitectureTemplate.Application.Features.Baskets.Queries.GetBasketById;
using CleanArchitectureTemplate.Application.RequestParameters;
using CleanArchitectureTemplate.Domain.Constants.StringConstants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
public class BasketsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BasketsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> GetAll([FromQuery] Pagination pagination, [FromQuery] bool includeBasketItems)
    {
        var request = new GetAllBasketsQueryRequest(pagination, includeBasketItems);
        return await _mediator.Send(request);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, [FromQuery] bool includeBasketItems)
    {
        var request = new GetBasketByIdQueryRequest(id, includeBasketItems);
        return await _mediator.Send(request);
    }

    [HttpPut("[action]/{id}")]
    public async Task<IActionResult> Clear([FromRoute] Guid id)
    {
        var request = new ClearBasketCommandRequest(id);
        return await _mediator.Send(request);
    }
}