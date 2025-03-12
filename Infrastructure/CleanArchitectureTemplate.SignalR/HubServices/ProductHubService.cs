using CleanArchitectureTemplate.Application.Abstractions.Hubs;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.SignalR.Clients;
using CleanArchitectureTemplate.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CleanArchitectureTemplate.SignalR.HubServices;

/// <summary>
/// Service implementation for managing real-time product-related operations via SignalR.
/// </summary>
public class ProductHubService : IProductHubService
{
    private readonly IHubContext<ProductHub, IProductClient> _hubContext;

    public ProductHubService(IHubContext<ProductHub, IProductClient> hubContext)
    {
        _hubContext = hubContext;
    }

    /// <inheritdoc/>
    public async Task SendProductAddedAsync(ProductDto productDto)
    {
        await _hubContext.Clients.All.ProductAddedAsync(productDto);
    }
}