using CleanArchitectureTemplate.Application.Common.Responses;
using MediatR;
using System;

namespace CleanArchitectureTemplate.Application.Features.Users.Commands.ResetPassword
{
    /// <summary>
    /// Represents a request to reset the user's password.
    /// </summary>
    /// <param name="UserId">The ID of the user.</param>
    /// <param name="Token">The password reset token.</param>
    /// <param name="NewPassword">The new password.</param>
    public record ResetPasswordCommandRequest(Guid UserId, string Token, string NewPassword) : IRequest<SingleResponse<bool>>;
}