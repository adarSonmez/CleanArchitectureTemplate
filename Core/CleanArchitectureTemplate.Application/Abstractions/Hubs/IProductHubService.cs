using CleanArchitectureTemplate.Application.Dtos.Marketing;

namespace CleanArchitectureTemplate.Application.Abstractions.Hubs;

/// <summary>
/// Interface for the product hub service to manage real-time product-related operations.
/// </summary>
public interface IProductHubService
{
    /// <summary>
    /// Sends a product added event to the clients.
    /// </summary>
    /// <param name="productDto">The product DTO to send.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task SendProductAddedAsync(ProductDto productDto);
}