namespace CommercePortal.Application.Dtos.Files;

using CommercePortal.Domain.Constants.Enums;
using CommercePortal.Domain.MarkerInterfaces;

/// <summary>
/// Represents the category image file data transfer object.
/// </summary>
public record FileDetailsDto
(
    Guid Id,
    string Name,
    string Extension,
    long Size,
    string HumanReadableSize,
    string Folder,
    StorageType Storage
) : IDto;