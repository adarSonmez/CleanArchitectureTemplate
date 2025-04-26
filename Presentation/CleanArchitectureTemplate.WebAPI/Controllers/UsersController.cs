using CleanArchitectureTemplate.Application.Features.Users.Commands.ChangePassword;
using CleanArchitectureTemplate.Application.Features.Users.Commands.ForgotPassword;
using CleanArchitectureTemplate.Application.Features.Users.Commands.RegisterUser;
using CleanArchitectureTemplate.Application.Features.Users.Commands.ResetPassword;
using CleanArchitectureTemplate.Application.Features.Users.Commands.UpdateUser;
using CleanArchitectureTemplate.Application.Features.Users.Queries.GetAllUsers;
using CleanArchitectureTemplate.Application.Features.Users.Queries.GetUserById;
using CleanArchitectureTemplate.Application.RequestParameters;
using CleanArchitectureTemplate.Domain.Constants.StringConstants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = UserRoles.Admin)]
    [ResponseCache(Duration = 360, Location = ResponseCacheLocation.Client, VaryByQueryKeys = ["*"])]
    public async Task<IActionResult> GetUsers([FromQuery] Pagination pagination)
    {
        var request = new GetAllUsersQueryRequest(pagination);
        return await _mediator.Send(request);
    }

    [HttpGet("{id}")]
    [Authorize]
    [ResponseCache(Duration = 360, Location = ResponseCacheLocation.Any)]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        var request = new GetUserByIdQueryRequest(id);
        return await _mediator.Send(request);
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommandRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommandRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPut("[action]")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommandRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommandRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommandRequest request)
    {
        return await _mediator.Send(request);
    }
}