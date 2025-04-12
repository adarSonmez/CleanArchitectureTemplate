using CleanArchitectureTemplate.Application.Abstractions.Hubs;
using CleanArchitectureTemplate.Application.Dtos.Shopping;
using CleanArchitectureTemplate.RealtimeCommunication.Clients;
using CleanArchitectureTemplate.RealtimeCommunication.Hubs.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace CleanArchitectureTemplate.RealtimeCommunication.HubServices.SignalR;

/// <summary>
/// Service implementation for managing real-time product-related operations via SignalR.
/// </summary>
public class SignalRProductHubService : IProductHubService
{
    private readonly IHubContext<SignalRProductHub, IProductClient> _hubContext;

    public SignalRProductHubService(IHubContext<SignalRProductHub, IProductClient> hubContext)
    {
        _hubContext = hubContext;
    }

    /// <inheritdoc/>
    public async Task SendProductAddedAsync(ProductDto productDto)
    {
        await _hubContext.Clients.All.ProductAddedAsync(productDto);
    }
}