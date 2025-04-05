using CleanArchitectureTemplate.Application.Dtos.Files;
using CleanArchitectureTemplate.Domain.Constants.SmartEnums.Files;
using CleanArchitectureTemplate.Domain.Entities.Files;

namespace CleanArchitectureTemplate.Application.Mappings.Files;

/// <summary>
/// Provides extension methods for mapping between <see cref="FileDetails"/> and <see cref="FileDetailsDto"/>.
/// </summary>
public static class FileDetailsMapper
{
    /// <summary>
    /// Maps a <see cref="FileDetails"/> entity to a <see cref="FileDetailsDto"/>.
    /// </summary>
    /// <param name="entity">The <see cref="FileDetails"/> to map.</param>
    /// <returns>The mapped <see cref="FileDetailsDto"/>.</returns>
    public static FileDetailsDto ToDto(this FileDetails entity)
    {
        if (entity == null) return null!;

        return new FileDetailsDto(
            Id: entity.Id,
            Name: entity.Name,
            Extension: entity.Extension.Extension, // Use the actual extension string like ".jpg"
            Size: entity.Size,
            HumanReadableSize: entity.HumanReadableSize,
            Folder: entity.Folder,
            Storage: entity.Storage
        );
    }

    /// <summary>
    /// Maps a <see cref="FileDetailsDto"/> to a <see cref="FileDetails"/> entity.
    /// </summary>
    /// <param name="dto">The <see cref="FileDetailsDto"/> to map.</param>
    /// <returns>The mapped <see cref="FileDetails"/> entity.</returns>
    public static FileDetails ToEntity(this FileDetailsDto dto)
    {
        if (dto == null) return null!;

        return new FileDetails
        {
            Id = dto.Id,
            Name = dto.Name,
            Extension = FileExtension.FromExtension(dto.Extension), // Use your smart enum logic
            Size = dto.Size,
            Folder = dto.Folder,
            Storage = dto.Storage
        };
    }
}