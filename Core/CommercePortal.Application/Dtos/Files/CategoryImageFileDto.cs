using CommercePortal.Domain.MarkerInterfaces;

namespace CommercePortal.Application.Dtos.Files;

/// <summary>
/// Represents the category image file data transfer object.
/// </summary>
public record CategoryImageFileDto
(
    Guid Id,
    FileDetailsDto? FileDetails
) : IDto;