namespace CleanArchitectureTemplate.Application.Abstractions.AI;

/// <summary>
/// Service for managing chat history.
/// </summary>
public interface IChatHistoryService
{
    /// <summary>
    /// Adds a message to the chat history for a connection ID.
    /// </summary>
    /// <param name="connectionId">The connection ID.</param>
    /// <param name="role">The role of the author.</param>
    /// <param name="content">The content of the message.</param>
    void AddMessage(string connectionId, object role, string content);

    /// <summary>
    /// Clears the chat history for a connection ID.
    /// </summary>
    /// <param name="connectionId">The connection ID.</param>
    void ClearHistory(string connectionId);

    /// <summary>
    /// Gets the chat history for a connection ID.
    /// </summary>
    /// <param name="connectionId">The connection ID.</param>
    /// <returns>The chat history.</returns>
    System.Collections.IEnumerable GetHistory(string connectionId);
}