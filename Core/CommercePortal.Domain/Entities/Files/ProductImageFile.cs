using CommercePortal.Domain.Common;
using CommercePortal.Domain.Entities.Marketing;

namespace CommercePortal.Domain.Entities.Files;

/// <summary>
/// Represents a product image file entity.
/// </summary>
public class ProductImageFile : BaseEntity
{
    /// <summary>
    /// Gets or sets the flag indicating whether the image is the main image of the product.
    /// </summary>
    /// <remarks>
    /// Only one image can be the main image for a product.
    /// </remarks>
    public bool IsPrimary { get; set; }

    /// <summary>
    /// Gets or sets the product that the image belongs to.
    public Product Product { get; set; } = default!;

    /// Gets or sets the file details.
    /// </summary>
    public FileDetails FileDetails { get; set; } = default!;
}