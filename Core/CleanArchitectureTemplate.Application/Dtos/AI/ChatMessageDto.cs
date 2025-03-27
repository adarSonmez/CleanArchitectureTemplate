using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Dtos.AI;

/// <summary>
/// Represents a chat message sent by the AI service.
/// </summary>
/// <param name="Role">The role of the message.</param>
/// <param name="Think">The think content. Null if streaming.</param>
/// <param name="Message">The message content. Null if streaming.</param>
/// <param name="ModelId">The model identifier.</param>
/// <param name="Streaming">Whether the response is streamed.</param>
public record ChatMessageDto
(
    string Role,
    string? Think,
    string? Message,
    string ModelId,
    bool Streaming
) : IDto;