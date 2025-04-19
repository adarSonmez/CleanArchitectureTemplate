using CleanArchitectureTemplate.Application.Dtos.Messaging.Declaration;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Dtos.Messaging.PubSub;

/// <summary>
/// Data Transfer Object for subscribing to messages with extended declarations.
/// </summary>
/// <typeparam name="T">The type of the message.</typeparam>
/// <param name="RoutingKey">The routing key for the message. Leave empty for fanout exchanges. For topic exchanges, use a wildcard pattern. (# for zero or multiple words, * for one word).</param>
/// <param name="OnMessage">The callback function to handle the message.</param>
/// <param name="AutoAck">Indicates whether to automatically acknowledge the message.</param>
/// <param name="Multiple">Indicates whether to acknowledge multiple messages.</param>
/// <param name="Requeue">Indicates whether to requeue the message on error or rejection.</param>
/// <param name="FairDispatch">Indicates whether to use fair dispatch for message delivery.</param>
/// <param name="QueueDeclaration">The queue declaration options. If not provided, a server-generated queue will be used.</param>
/// <param name="CancellationToken">The cancellation token for the operation.</param>
public record SubscribeMessageDto<T>(
    Func<T, Task> OnMessage,
    ExchangeDeclarationDto ExchangeDeclaration,
    string? RoutingKey = null,
    bool AutoAck = false,
    bool Multiple = false,
    bool Requeue = true,
    bool FairDispatch = false,
    QueueDeclarationDto? QueueDeclaration = null,
    CancellationToken CancellationToken = default
) : IDto;