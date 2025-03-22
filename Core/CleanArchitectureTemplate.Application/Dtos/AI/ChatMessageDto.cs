using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Dtos.AI;

/// <summary>
/// Represents a chat message sent by the AI service.
/// </summary>
/// <param name="Role">The role of the message.</param>
/// <param name="Message">The message content.</param>
/// <param name="ModelId">The model identifier.</param>
public record ChatMessageDto
(
    string Role,
    string? Think,
    string Message,
    string ModelId
) : IDto;