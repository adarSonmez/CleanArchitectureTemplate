namespace CommercePortal.Application.Features.Commands.AppUsers.CreateUser;

/// <summary>
/// Represents the response of the <see cref="CreateAppUserCommandRequest"/>.
/// </summary>
/// <param name="Id">The identifier of the created user.</param>
public record CreateAppUserCommandResponse(Guid Id);