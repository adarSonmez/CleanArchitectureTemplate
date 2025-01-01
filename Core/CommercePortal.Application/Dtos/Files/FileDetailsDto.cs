namespace CommercePortal.Application.Dtos.Files;

using CommercePortal.Domain.Constants.Enums;
using CommercePortal.Domain.MarkerInterfaces;

/// <summary>
/// Represents the category image file data transfer object.
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="Name">The name of the file.</param>
/// <param name="Extension">The extension of the file.</param>
/// <param name="Size">The size of the file in bytes.</param>
/// <param name="HumanReadableSize">The size of the file in human readable format.</param>
/// <param name="Folder">The folder name where the file is stored.</param>
/// <param name="Storage">The storage type.</param>
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