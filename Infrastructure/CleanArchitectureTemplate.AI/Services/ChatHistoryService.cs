using CleanArchitectureTemplate.Application.Abstractions.AI;
using CleanArchitectureTemplate.Application.Dtos.AI;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Collections.Concurrent;

namespace CleanArchitectureTemplate.AI.Services;

/// <summary>
/// Implementation of the chat history service using Semantic Kernel.
/// </summary>
public class SemanticKernelChatHistoryService : IChatHistoryService
{
    private readonly ConcurrentDictionary<string, ChatHistory> _chatHistories = new();

    /// <inheritdoc />
    public void ClearHistory(string connectionId)
    {
        _chatHistories.TryRemove(connectionId, out _);
    }

    /// <inheritdoc />
    public void AddMessage(string connectionId, object role, string content)
    {
        if (_chatHistories.TryGetValue(connectionId, out ChatHistory? history))
        {
            history.AddMessage((AuthorRole)role, content);
        }
        else
        {
            history = [];
            history.AddMessage((AuthorRole)role, content);
            _chatHistories.TryAdd(connectionId, history);
        }
    }

    /// <inheritdoc />
    System.Collections.IEnumerable IChatHistoryService.GetHistory(string connectionId)
    {
        if (_chatHistories.TryGetValue(connectionId, out ChatHistory? history))
        {
            return history;
        }
        return new ChatHistory();
    }
}