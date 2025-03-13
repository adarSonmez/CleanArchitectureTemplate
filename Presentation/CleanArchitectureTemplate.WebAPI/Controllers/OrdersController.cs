using CleanArchitectureTemplate.Application.Features.Orders.Commands.CreateOrderFromBasket;
using CleanArchitectureTemplate.Application.Features.Orders.Commands.UpdateOrderStatus;
using CleanArchitectureTemplate.Application.Features.Orders.Queries.GetAllOrders;
using CleanArchitectureTemplate.Application.Features.Orders.Queries.GetOrderById;
using CleanArchitectureTemplate.Application.Features.Orders.Queries.GetOrdersByCustomerId;
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
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = UserRoles.Admin)]
    [ResponseCache(Duration = 360, Location = ResponseCacheLocation.Client, VaryByQueryKeys = ["*"])]
    public async Task<IActionResult> GetAll([FromQuery] Pagination pagination, [FromQuery] bool includeBasket)
    {
        var request = new GetAllOrdersQueryRequest(pagination, includeBasket);
        return await _mediator.Send(request);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, [FromQuery] bool includeOrderItems)
    {
        var request = new GetOrderByIdQueryRequest(id, includeOrderItems);
        return await _mediator.Send(request);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetByCustomer([FromBody] Pagination pagination, [FromQuery] bool includeBasket)
    {
        var request = new GetOrdersByCustomerIdQueryRequest(pagination, includeBasket);
        return await _mediator.Send(request);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderFromBasketCommandRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPatch("[action]")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> UpdateStatus([FromBody] UpdateOrderStatusCommandRequest request)
    {
        return await _mediator.Send(request);
    }
}