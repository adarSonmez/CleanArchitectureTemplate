using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Exceptions;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Users.Commands.ChangePassword;

/// <summary>
/// Handles the change password request.
/// </summary>
public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommandRequest, ResponseResult>
{
    private readonly IUserService _userService;
    private readonly IUserContextService _userContextService;

    public ChangePasswordCommandHandler(IUserService userService, IUserContextService userContextService)
    {
        _userService = userService;
        _userContextService = userContextService;
    }

    public async Task<ResponseResult> Handle(ChangePasswordCommandRequest request, CancellationToken cancellationToken)
    {
        if (!_userContextService.IsCurrentUser(request.UserId))
            throw new ForbiddenException();

        await _userService.ChangePasswordAsync(request.UserId, request.CurrentPassword, request.NewPassword);

        return new();
    }
}