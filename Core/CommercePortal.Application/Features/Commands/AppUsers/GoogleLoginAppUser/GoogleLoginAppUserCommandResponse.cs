using CommercePortal.Application.DTOs;

namespace CommercePortal.Application.Features.Commands.AppUsers.GoogleLoginAppUser;

/// <summary>
/// Represents the response of the <see cref="GoogleLoginAppUserCommandRequest"/>.
/// </summary>
/// <param name="Token">The token generated for the user.</param>
public record GoogleLoginAppUserCommandResponse(Token Token);