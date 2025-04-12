using CleanArchitectureTemplate.Application.Abstractions.Hubs;
using CleanArchitectureTemplate.Application.Dtos.AI;
using CleanArchitectureTemplate.RealtimeCommunication.Clients;
using CleanArchitectureTemplate.RealtimeCommunication.Hubs.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace CleanArchitectureTemplate.RealtimeCommunication.HubServices.SignalR;

/// <summary>
/// Service implementation for managing real-time order-related operations via SignalR.
/// </summary>
public class SignalRAIHubService : IAIHubService
{
    private readonly IHubContext<SignalRAIHub, IAIClient> _hubContext;

    public SignalRAIHubService(IHubContext<SignalRAIHub, IAIClient> hubContext)
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