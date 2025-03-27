using CleanArchitectureTemplate.Application.Features.AI.Queries.Chat;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
public class AIController : ControllerBase
{
    private readonly IMediator _mediator;

    public AIController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Chat([FromBody] ChatQueryRequest request)
    {
        return await _mediator.Send(request);
    }
}