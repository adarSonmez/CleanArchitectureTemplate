using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Dtos.Messaging.PubSub;

/// <summary>
/// Represents custom message properties for publishing.
/// </summary>
/// <param name="ContentType">The MIME content type of the message.</param>
/// <param name="Headers">Custom headers to attach to the message.</param>
/// <param name="Expiration">Time (in milliseconds) for the message to live in the queue.</param>
/// <param name="Priority">The priority of the message.</param>
public record MessagePropertiesDto(
    string? ContentType = null,
    IDictionary<string, object?>? Headers = null,
    string? Expiration = null,
    byte Priority = default
) : IDto;