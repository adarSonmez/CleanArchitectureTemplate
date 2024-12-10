namespace CommercePortal.Domain.Entities;

/// <summary>
/// Represents a product image file entity.
/// </summary>
public class ProductImageFile : File
{
    /// <summary>
    /// Gets or sets the product ID.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the product.
    /// </summary>
    public Product Product { get; set; } = default!;
}