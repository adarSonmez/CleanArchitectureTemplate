using CleanArchitectureTemplate.Application.Dtos.AI;

namespace CleanArchitectureTemplate.Application.Abstractions.AI;

/// <summary>
/// Central AI service abstraction.
/// </summary>
public interface IAiService
{
    /// <summary>
    /// Sends a message to the AI service and receives a response.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <returns>The response from the AI service.</returns>
    Task<ChatMessageDto?> SendMessageAsync(string message);
}