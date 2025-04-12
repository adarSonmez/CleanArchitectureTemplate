using CleanArchitectureTemplate.Application.Abstractions.Hubs;
using CleanArchitectureTemplate.Application.Dtos.Ordering;
using CleanArchitectureTemplate.RealtimeCommunication.Clients;
using CleanArchitectureTemplate.RealtimeCommunication.Hubs.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace CleanArchitectureTemplate.RealtimeCommunication.HubServices.SignalR;

/// <summary>
/// Service implementation for managing real-time order-related operations via SignalR.
/// </summary>
public class SignalROrderHubService : IOrderHubService
{
    private readonly IHubContext<SignalROrderHub, IOrderClient> _hubContext;

    public SignalROrderHubService(IHubContext<SignalROrderHub, IOrderClient> hubContext)
    {
        _hubContext = hubContext;
    }

    /// <inheritdoc/>
    public async Task SendOrderCreatedAsync(OrderDto orderDto)
    {
        await _hubContext.Clients.All.OrderCreatedAsync(orderDto);
    }
}