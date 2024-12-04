namespace CommercePortal.Application.ViewModels.Products;

/// <summary>
/// Represents a view model for creating a product.
/// </summary>
public class CreateProductVM
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public int? Stock { get; set; } = default!;
    public decimal Price { get; set; } = default!;
}