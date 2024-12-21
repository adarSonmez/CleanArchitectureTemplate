using CommercePortal.Domain.Common;
using CommercePortal.Domain.Entities.Marketing;

namespace CommercePortal.Domain.Entities.Files;

/// <summary>
/// Represents a category image file entity.
/// </summary>
public class CategoryImageFile : BaseEntity
{
    /// <summary>
    /// Gets or sets the category.
    /// </summary>
    public Category Category { get; set; } = default!;

    /// <summary>
    /// Gets or sets the file details.
    /// </summary>
    public FileDetails FileDetails { get; set; } = default!;
}