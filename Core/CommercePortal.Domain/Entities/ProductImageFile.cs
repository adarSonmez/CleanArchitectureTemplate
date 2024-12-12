namespace CommercePortal.Domain.Entities;

/// <summary>
/// Represents a product image file entity.
/// </summary>
public class ProductImageFile : File
{
    /// <summary>
    /// Gets or sets the product.
    /// </summary>
    public ICollection<Product> Products { get; set; } = [];
}