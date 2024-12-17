using CommercePortal.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CommercePortal.Application.Features.Commands.AppUsers.CreateUser;

/// <summary>
/// Represents a handler for the <see cref="CreateAppUserCommandRequest"/>
/// </summary>
public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommandRequest, CreateAppUserCommandResponse>
{
    public readonly UserManager<AppUser> _userManager;

    public CreateAppUserCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CreateAppUserCommandResponse> Handle(CreateAppUserCommandRequest request, CancellationToken cancellationToken)
    {
        var user = new AppUser
        {
            FullName = request.FullName,
            Email = request.Email,
            UserName = request.UserName,
            PhoneNumber = request.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            return new CreateAppUserCommandResponse(user.Id);
        }

        if (result.Errors.Any())
        {
            var errors = result.Errors.Select(e => e.Description);
            throw new Exception($"Failed to create user: {string.Join(", ", errors)}");
        }
        else
        {
            throw new Exception("Failed to create user");
        }
    }
}