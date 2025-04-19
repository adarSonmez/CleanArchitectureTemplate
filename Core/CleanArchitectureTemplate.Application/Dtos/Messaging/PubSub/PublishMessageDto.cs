using CleanArchitectureTemplate.Application.Dtos.Messaging.Declaration;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Dtos.Messaging.PubSub;

/// <summary>
/// Data Transfer Object for publishing messages with extended declarations.
/// </summary>
/// <typeparam name="T">The type of the message.</typeparam>
/// <param name="Message">The message to be published.</param>
/// <param name="RoutingKey">The routing key for the message. Leave empty for fanout exchanges. For topic exchanges, use a wildcard pattern. (# for zero or multiple words, * for one word).</param>
/// <param name="PersistentMessages">Indicates whether the messages are persistent.</param>
/// <param name="Mandatory">Indicates whether the message must route to a queue or not.</param>
/// <param name="ExchangeDeclaration">Optional exchange declaration options.</param>
/// <param name="MessageProperties">Optional custom properties for the message.</param>
/// <param name="CancellationToken">The cancellation token for the operation.</param>
public record PublishMessageDto<T>(
    T Message,
    ExchangeDeclarationDto ExchangeDeclaration,
    string? RoutingKey = null,
    bool PersistentMessages = true,
    bool Mandatory = false,
    MessagePropertiesDto? MessageProperties = null,
    CancellationToken CancellationToken = default
) : IDto;