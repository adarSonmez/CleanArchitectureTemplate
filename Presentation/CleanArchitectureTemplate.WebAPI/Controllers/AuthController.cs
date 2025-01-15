using CleanArchitectureTemplate.Application.Features.AppUsers.Commands.LoginAppUser;
using CleanArchitectureTemplate.Application.Features.Commands.AppUsers.FacebookLoginAppUser;
using CleanArchitectureTemplate.Application.Features.Commands.AppUsers.GoogleLoginAppUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
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

    [HttpPost("[action]")]
    public async Task<IActionResult> FacobookLogin([FromBody] FacebookLoginAppUserCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}