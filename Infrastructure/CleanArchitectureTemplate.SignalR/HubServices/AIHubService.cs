using CleanArchitectureTemplate.Application.Abstractions.Hubs;
using CleanArchitectureTemplate.Application.Dtos.AI;
using CleanArchitectureTemplate.SignalR.Clients;
using CleanArchitectureTemplate.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CleanArchitectureTemplate.SignalR.HubServices;

/// <summary>
/// Service implementation for managing real-time order-related operations via SignalR.
/// </summary>
public class AIHubService : IAIHubService
{
    private readonly IHubContext<AIHub, IAIClient> _hubContext;

    public AIHubService(IHubContext<AIHub, IAIClient> hubContext)
    {
        _hubContext = hubContext;
    }

    /// <inheritdoc/>
    public async Task AITypingAsync(string message, string connectionId, bool thinking)
    {
        await _hubContext.Clients.Client(connectionId).ReceiveAITypingAsync(message, thinking);
    }

    /// <inheritdoc/>
    public async Task SendAIResponseAsync(ChatMessageDto messageDto, string connectionId)
    {
        await _hubContext.Clients.Client(connectionId).ReceiveAIResponseAsync(messageDto);
    }
}