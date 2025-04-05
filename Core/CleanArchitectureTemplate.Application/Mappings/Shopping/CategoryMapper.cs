using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Shopping;

namespace CleanArchitectureTemplate.Application.Mappings.Shopping;

/// <summary>
/// Provides extension methods for mapping between <see cref="Category"/> and <see cref="CategoryDto"/>.
/// </summary>
public static class CategoryMapper
{
    /// <summary>
    /// Maps a <see cref="Category"/> entity to a <see cref="CategoryDto"/>.
    /// </summary>
    /// <param name="category">The <see cref="Category"/> entity to map.</param>
    /// <returns>The mapped <see cref="CategoryDto"/>.</returns>
    public static CategoryDto ToDto(this Category category)
    {
        if (category == null) return null!;

        return new CategoryDto(
            Id: category.Id,
            Name: category.Name,
            Description: category.Description,
            ParentCategoryId: category.ParentCategoryId,
            Products: category.Products?.Select(p => p.ToDto()).ToList()
        );
    }
}