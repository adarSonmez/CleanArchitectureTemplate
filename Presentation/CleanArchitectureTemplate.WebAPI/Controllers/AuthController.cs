using CleanArchitectureTemplate.Application.Features.Auth.Commands.InternalLogin;
using CleanArchitectureTemplate.Application.Features.Auth.Commands.FacebookLogin;
using CleanArchitectureTemplate.Application.Features.Auth.Commands.GoogleLogin;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CleanArchitectureTemplate.Application.Features.Auth.Commands.RefreshToken;

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
    public async Task<IActionResult> Login([FromBody] InternalLoginCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> FacobookLogin([FromBody] FacebookLoginCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}