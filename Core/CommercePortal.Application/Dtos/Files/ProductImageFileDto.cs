namespace CommercePortal.Application.Dtos.Files;

using CommercePortal.Domain.MarkerInterfaces;

/// <summary>
/// Represents the product image file data transfer object.
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="FileDetails">The file details data transfer object.</param>
public record ProductImageFileDto
(
    Guid Id,
    FileDetailsDto? FileDetails
) : IDto;