using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Exceptions;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Users.Commands.ForgotPassword;

/// <summary>
/// Handles the forgot password request.
/// </summary>
public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommandRequest, SingleResponse<bool>>
{
    private readonly IUserService _userService;

    public ForgotPasswordCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<SingleResponse<bool>> Handle(ForgotPasswordCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<bool>();

        var success = await _userService.ForgotPasswordAsync(request.Email);

        if (!success)
        {
            throw new ServiceUnavailableException("Failed to generate password reset token.");
        }

        response.SetData(true);
        return response;
    }
}