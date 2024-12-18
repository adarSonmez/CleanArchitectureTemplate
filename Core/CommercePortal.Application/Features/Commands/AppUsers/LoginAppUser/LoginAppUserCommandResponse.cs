using CommercePortal.Application.DTOs;

namespace CommercePortal.Application.Features.Commands.AppUsers.LoginAppUser;

/// <summary>
/// Represents the response of the <see cref="LoginAppUserCommandRequest"/>.
/// </summary>
/// <param name="Token">The token generated for the user.</param>
public record LoginAppUserCommandResponse(Token Token);