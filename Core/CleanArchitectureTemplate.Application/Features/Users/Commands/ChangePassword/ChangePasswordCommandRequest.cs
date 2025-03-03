using CleanArchitectureTemplate.Application.Common.Responses;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Users.Commands.ChangePassword;

/// <summary>
/// Represents a request to change the user's password.
/// </summary>
/// <param name="UserId">The ID of the user</param>
/// <param name="CurrentPassword">The current password of the user</param>
/// <param name="NewPassword">The new password for the user</param>
/// <param name="ConfirmNewPassword">Confirmation of the new password</param>
public record ChangePasswordCommandRequest
(
    Guid UserId,
    string CurrentPassword,
    string NewPassword,
    string ConfirmNewPassword
) : IRequest<ResponseResult>;