namespace CommercePortal.Application.Features.Commands.AppUsers.RegisterAppUser;

/// <summary>
/// Represents the response of the <see cref="RegisterAppUserCommandRequest"/>.
/// </summary>
/// <param name="Id">The identifier of the created user.</param>
public record RegisterAppUserCommandResponse(Guid Id);