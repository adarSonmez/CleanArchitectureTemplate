using CommercePortal.Domain.Common;
using CommercePortal.Domain.Entities.Files;

namespace CommercePortal.Domain.Entities.Marketing;

/// <summary>
/// Represents a category entity.
/// </summary>
public class Category : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the category.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the category.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets foreign key for the parent category.
    /// </summary>
    public Guid? ParentCategoryId { get; set; }

    /// <summary>
    /// Gets or sets the parent category.
    /// </summary>
    public Category? ParentCategory { get; set; }

    /// <summary>
    /// Gets or sets foreign key for the image of the category.
    /// </summary>
    public Guid CategoryImageFileId { get; set; }

    /// <summary>
    /// Gets or sets the image of the category.
    /// </summary>
    public CategoryImageFile CategoryImageFile { get; set; } = default!;

    /// <summary>
    /// Gets or sets the products in the category.
    /// </summary>
    public ICollection<Product> Products { get; set; } = [];
}