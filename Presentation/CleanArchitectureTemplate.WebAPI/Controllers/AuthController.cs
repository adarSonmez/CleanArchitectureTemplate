using CleanArchitectureTemplate.Application.Features.Auth.Commands.FacebookLogin;
using CleanArchitectureTemplate.Application.Features.Auth.Commands.GoogleLogin;
using CleanArchitectureTemplate.Application.Features.Auth.Commands.InternalLogin;
using CleanArchitectureTemplate.Application.Features.Auth.Commands.Logout;
using CleanArchitectureTemplate.Application.Features.Auth.Commands.RefreshToken;
using CleanArchitectureTemplate.Application.Features.Auth.Commands.RevokeRefreshToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #region Login Actions

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] InternalLoginCommandRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginCommandRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> FacobookLogin([FromBody] FacebookLoginCommandRequest request)
    {
        return await _mediator.Send(request);
    }

    #endregion Login Actions

    #region Token Actions

    [HttpPost("[action]")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommandRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> RevokeRefreshToken()
    {
        return await _mediator.Send(new RevokeRefreshTokenCommandRequest());
    }

    #endregion Token Actions

    #region Logout Actions

    [HttpPost("[action]")]
    public async Task<IActionResult> Logout()
    {
        return await _mediator.Send(new LogoutCommandRequest());
    }

    #endregion Logout Actions
}