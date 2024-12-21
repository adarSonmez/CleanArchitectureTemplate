using CommercePortal.Domain.Common;
using CommercePortal.Domain.Entities.Marketing;

namespace CommercePortal.Domain.Entities.Files;

/// <summary>
/// Represents a product image file entity.
/// </summary>
public class ProductImageFile : BaseEntity
{
    /// <summary>
    /// Gets or sets the products associated with the file.
    /// </summary>
    public ICollection<Product> Products { get; set; } = [];

    /// <summary>
    /// Gets or sets the file details.
    /// </summary>
    public FileDetails FileDetails { get; set; } = default!;
}