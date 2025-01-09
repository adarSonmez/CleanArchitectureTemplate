namespace CleanArchitectureTemplate.Application.Dtos.Files;

using CleanArchitectureTemplate.Domain.Constants.Enums;
using CleanArchitectureTemplate.Domain.Entities.Files;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;

/// <summary>
/// Represents data transfer object for <see cref="FileDetails"/>
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