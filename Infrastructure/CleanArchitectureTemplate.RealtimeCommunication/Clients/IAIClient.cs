using CleanArchitectureTemplate.Application.Dtos.AI;

namespace CleanArchitectureTemplate.RealtimeCommunication.Clients;

/// <summary>
/// Client for managing real-time chat operations.
/// </summary>
public interface IAIClient
{
    /// <summary>
    /// Sends a message to the chat client.
    /// </summary>
    /// <param name="connectionId">The connection ID of the client.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ClientConnectedAsync(string connectionId);

    /// <summary>
    /// Sends a message to the chat client.
    /// </summary>
    /// <param name="connectionId">The connection ID of the client.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ClientDisconnectedAsync(string connectionId);

    /// <summary>
    /// Called when the AI is typing or thinking.
    /// </summary>
    /// <param name="message">Live text or thinking state</param>
    /// <param name="thinking">True if the AI is "thinking", false if actively typing.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ReceiveAITypingAsync(string message, bool thinking);

    /// <summary>
    /// Called when the AI has completed its response.
    /// </summary>
    /// <param name="messageDto">The AI response message.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ReceiveAIResponseAsync(ChatMessageDto messageDto);
}