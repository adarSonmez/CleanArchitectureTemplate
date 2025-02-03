using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Exceptions;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Users.Commands.ChangePassword;

/// <summary>
/// Handles the change password request.
/// </summary>
public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommandRequest, SingleResponse<bool>>
{
    private readonly IUserService _userService;

    public ChangePasswordCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<SingleResponse<bool>> Handle(ChangePasswordCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<bool>();
        var success = await _userService.ChangePasswordAsync(request.UserId, request.CurrentPassword, request.NewPassword);

        if (!success)
        {
            throw new UnauthorizedException("Password change failed.");
        }

        response.SetData(true);
        return response;
    }
}