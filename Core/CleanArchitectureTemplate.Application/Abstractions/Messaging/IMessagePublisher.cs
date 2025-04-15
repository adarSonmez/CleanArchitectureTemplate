using CleanArchitectureTemplate.Application.Dtos.Messaging.PubSub;

namespace CleanArchitectureTemplate.Application.Abstractions.Messaging;

/// <summary>
/// Abstraction for publishing messages to a messaging system.
/// </summary>
public interface IMessagePublisher
{
    /// <summary>
    /// Publishes the given message asynchronously.
    /// </summary>
    /// <typeparam name="T">The message type.</typeparam>
    /// <param name="messageDto">The message to be published.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task PublishAsync<T>(PublishMessageDto<T> messageDto) where T : class;
}