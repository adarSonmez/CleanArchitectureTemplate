using CommercePortal.Application.Features.Commands.AppUsers.GoogleLoginAppUser;
using CommercePortal.Application.Features.Commands.AppUsers.LoginAppUser;
using CommercePortal.Application.Features.Commands.AppUsers.RegisterAppUser;
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

    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromBody] RegisterAppUserCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] LoginAppUserCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginAppUserCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}