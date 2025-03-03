using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Users.Commands.ForgotPassword;

/// <summary>
/// Handles the forgot password request.
/// </summary>
public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommandRequest, ResponseResult>
{
    private readonly IUserService _userService;

    public ForgotPasswordCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<ResponseResult> Handle(ForgotPasswordCommandRequest request, CancellationToken cancellationToken)
    {
        await _userService.ForgotPasswordAsync(request.Email);

        return new();
    }
}