﻿using CleanArchitectureTemplate.Application.Abstractions.AI;
using CleanArchitectureTemplate.SignalR.Clients;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace CleanArchitectureTemplate.SignalR.Hubs;

/// <summary>
/// Hub for managing real-time chat operations.
/// </summary>
public class AIHub : Hub<IAIClient>
{
    private readonly ILogger<AIHub> _logger;
    private readonly IChatHistoryService _chatHistoryService;

    public AIHub(ILogger<AIHub> logger, IChatHistoryService chatHistoryService)
    {
        _logger = logger;
        _chatHistoryService = chatHistoryService;
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
        _chatHistoryService.ClearHistory(Context.ConnectionId);
        await Clients.All.ClientDisconnectedAsync(Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }
}