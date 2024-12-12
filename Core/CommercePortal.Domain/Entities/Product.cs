using CommercePortal.Domain.Entities.Common;

namespace CommercePortal.Domain.Entities;

/// <summary>
/// Represents a product entity.
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the stock of the product.
    /// </summary>
    public int Stock { get; set; }

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    public required decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the orders which include the product.
    /// </summary>
    public ICollection<Order> Orders { get; set; } = [];

    /// <summary>
    /// Gets or sets the product image files.
    /// </summary>
    public ICollection<ProductImageFile> ProductImageFiles { get; set; } = [];
}