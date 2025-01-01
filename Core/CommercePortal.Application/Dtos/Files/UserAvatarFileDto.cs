namespace CommercePortal.Application.Dtos.Files;

using CommercePortal.Domain.MarkerInterfaces;

/// <summary>
/// Represents the user avatar file data transfer object.
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="FileDetails">The file details data transfer object.</param>
/// <param name="UserId">The identifier of the user who owns the avatar.</param>
public record UserAvatarFileDto
(
    Guid Id,
    FileDetailsDto? FileDetails,
    Guid UserId
) : IDto;