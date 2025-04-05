using CleanArchitectureTemplate.Application.Dtos.Files;
using CleanArchitectureTemplate.Domain.Entities.Files;

namespace CleanArchitectureTemplate.Application.Mappings.Files;

/// <summary>
/// Provides extension methods for mapping between <see cref="ProductImageFile"/> and <see cref="ProductImageFileDto"/>.
/// </summary>
public static class ProductImageFileMapper
{
    /// <summary>
    /// Maps a <see cref="ProductImageFile"/> entity to a <see cref="ProductImageFileDto"/>.
    /// </summary>
    /// <param name="entity">The <see cref="ProductImageFile"/> to map.</param>
    /// <returns>The mapped <see cref="ProductImageFileDto"/>.</returns>
    public static ProductImageFileDto ToDto(this ProductImageFile entity)
    {
        if (entity == null) return null!;

        return new ProductImageFileDto(
            Id: entity.Id,
            ProductId: entity.ProductId,
            IsPrimary: entity.IsPrimary,
            FileDetails: entity.FileDetails?.ToDto()
        );
    }

    /// <summary>
    /// Maps a <see cref="ProductImageFileDto"/> to a <see cref="ProductImageFile"/> entity.
    /// </summary>
    /// <param name="dto">The <see cref="ProductImageFileDto"/> to map.</param>
    /// <returns>The mapped <see cref="ProductImageFile"/> entity.</returns>
    public static ProductImageFile ToEntity(this ProductImageFileDto dto)
    {
        if (dto == null) return null!;

        return new ProductImageFile
        {
            Id = dto.Id,
            ProductId = dto.ProductId,
            IsPrimary = dto.IsPrimary,
            FileDetails = dto.FileDetails?.ToEntity()
        };
    }
}