namespace CommercePortal.Application.Dtos.Files;

using CommercePortal.Domain.MarkerInterfaces;

/// <summary>
/// Represents the user avatar file data transfer object.
/// </summary>
public record UserAvatarFileDto
(
    Guid Id,
    FileDetailsDto? FileDetails,
    Guid UserId
) : IDto;