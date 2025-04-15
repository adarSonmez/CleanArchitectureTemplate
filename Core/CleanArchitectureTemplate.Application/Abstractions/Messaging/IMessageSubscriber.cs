using CleanArchitectureTemplate.Application.Dtos.Messaging.PubSub;

namespace CleanArchitectureTemplate.Application.Abstractions.Messaging;

/// <summary>
/// Abstraction for subscribing and processing messages from a messaging system.
/// </summary>
public interface IMessageSubscriber
{
    /// <summary>
    /// Subscribes to messages of type T using the specified routing key.
    /// </summary>
    /// <typeparam name="T">The type of the message.</typeparam>
    /// <param name="messageDto">The message subscription details.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<string> SubscribeAsync<T>(SubscribeMessageDto<T> messageDto) where T : class;

    /// <summary>
    /// Unsubscribes from messages using the specified consumer tag.
    /// </summary>
    public Task UnsubscribeAsync(string consumerTag, CancellationToken cancellationToken = default);
}