using CleanArchitectureTemplate.Domain.Entities.Files;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Dtos.Files;

/// <summary>
/// Represents data transfer object for <see cref="UserAvatarFile"/>
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="FileDetails">The file details data transfer object.</param>
/// <param name="UserId">The identifier of the user who owns the avatar.</param>
public record UserAvatarFileDto
(
    Guid Id,
    Guid UserId,
    FileDetailsDto? FileDetails
) : IDto;