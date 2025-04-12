using CleanArchitectureTemplate.RealtimeCommunication.Clients;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace CleanArchitectureTemplate.RealtimeCommunication.Hubs.SignalR;

/// <summary>
/// Hub for managing real-time communication related to  orders.
/// </summary>
public class SignalROrderHub : Hub<IOrderClient>
{
    private readonly ILogger<SignalROrderHub> _logger;

    public SignalROrderHub(ILogger<SignalROrderHub> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation($"Client connected: {Context.ConnectionId}");
        await Clients.All.ClientConnectedAsync(Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    /// <inheritdoc/>
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation($"Client disconnected: {Context.ConnectionId}");
        await Clients.All.ClientDisconnectedAsync(Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }
}