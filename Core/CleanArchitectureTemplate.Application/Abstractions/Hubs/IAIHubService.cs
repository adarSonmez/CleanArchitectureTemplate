using CleanArchitectureTemplate.Application.Dtos.AI;

namespace CleanArchitectureTemplate.Application.Abstractions.Hubs;

/// <summary>
/// Interface for the real-time chat hub service.
/// </summary>
public interface IAIHubService
{
    /// <summary>
    /// Sends reasoning and typing phase messages as chunks to the clients.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="connectionId">The connection ID to send the message to.</param>
    /// <param name="thinking">Whether the AI is thinking.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AITypingAsync(string message, string connectionId, bool thinking);

    /// <summary>
    /// Sends complete AI response to the clients.
    /// </summary>
    /// <param name="messageDto">The message DTO to send.</param>
    /// <param name="connectionId">The connection ID to send the message to.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendAIResponseAsync(ChatMessageDto messageDto, string connectionId);
}