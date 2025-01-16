﻿using CleanArchitectureTemplate.Application.Features.AppUsers.Commands.RegisterAppUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromBody] RegisterAppUserCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}