using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Dtos.Messaging.Declaration;

/// <summary>
/// Represents options for declaring a queue with extended configuration.
/// </summary>
/// <param name="QueueName">The name of the queue.</param>
/// <param name="Durable">Specifies whether the queue should survive a broker restart.</param>
/// <param name="Exclusive">Indicates whether the queue is limited to the connection.</param>
/// <param name="AutoDelete">Specifies whether the queue should be automatically deleted when not in use.</param>
/// <param name="Arguments">Additional arguments for advanced RabbitMQ features (e.g., dead-letter exchange, TTL, priority, etc.).</param>
public record QueueDeclarationDto(
    string QueueName,
    bool Durable = true,
    bool Exclusive = false,
    bool AutoDelete = false,
    IDictionary<string, object?>? Arguments = null
) : IDto;