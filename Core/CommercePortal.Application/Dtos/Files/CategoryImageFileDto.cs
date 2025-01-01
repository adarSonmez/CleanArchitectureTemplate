using CommercePortal.Domain.MarkerInterfaces;

namespace CommercePortal.Application.Dtos.Files;

/// <summary>
/// Represents the category image file data transfer object.
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="FileDetails">The file details data transfer object.</param>
public record CategoryImageFileDto
(
    Guid Id,
    FileDetailsDto? FileDetails
) : IDto;