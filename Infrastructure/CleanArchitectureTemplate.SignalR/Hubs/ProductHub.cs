﻿using CleanArchitectureTemplate.SignalR.Clients;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace CleanArchitectureTemplate.SignalR.Hubs;

/// <summary>
/// Hub for managing real-time communication related to products.
/// </summary>
public class ProductHub : Hub<IProductClient>
{
    private readonly ILogger<ProductHub> _logger;

    public ProductHub(ILogger<ProductHub> logger)
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