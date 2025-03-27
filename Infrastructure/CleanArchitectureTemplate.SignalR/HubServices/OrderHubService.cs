using CleanArchitectureTemplate.Application.Abstractions.Hubs;
using CleanArchitectureTemplate.Application.Dtos.Ordering;
using CleanArchitectureTemplate.SignalR.Clients;
using CleanArchitectureTemplate.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CleanArchitectureTemplate.SignalR.HubServices;

/// <summary>
/// Service implementation for managing real-time order-related operations via SignalR.
/// </summary>
public class OrderHubService : IOrderHubService
{
    private readonly IHubContext<OrderHub, IOrderClient> _hubContext;

    public OrderHubService(IHubContext<OrderHub, IOrderClient> hubContext)
    {
        _hubContext = hubContext;
    }

    /// <inheritdoc/>
    public async Task SendOrderCreatedAsync(OrderDto orderDto)
    {
        await _hubContext.Clients.All.OrderCreatedAsync(orderDto);
    }
}