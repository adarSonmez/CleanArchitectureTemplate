using CommercePortal.Application.Features.Commands.AppUsers.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CommercePortal.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppUserCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}