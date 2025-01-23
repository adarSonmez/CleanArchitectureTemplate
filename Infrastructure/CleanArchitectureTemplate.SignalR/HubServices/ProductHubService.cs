using CleanArchitectureTemplate.Application.Abstractions.Hubs;
using CleanArchitectureTemplate.Application.Dtos.Marketing;
using CleanArchitectureTemplate.SignalR.Constants;
using CleanArchitectureTemplate.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CleanArchitectureTemplate.SignalR.HubServices;

/// <summary>
/// Service implementation for managing real-time product-related operations via SignalR.
/// </summary>
public class ProductHubService : IProductHubService
{
    private readonly IHubContext<ProductHub> _hubContext;

    public ProductHubService(IHubContext<ProductHub> hubContext)
    {
        _hubContext = hubContext;
    }

    /// <inheritdoc/>
    public Task SendProductAddedAsync(ProductDto productDto)
    {
        var message = $"Product added: {productDto.Name} with ID: {productDto.Id}";
        return _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.ProductAdded, message);
    }
}