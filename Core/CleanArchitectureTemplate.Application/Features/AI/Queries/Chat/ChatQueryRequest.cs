using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.AI;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.AI.Queries.Chat;

/// <summary>
/// Request to send a message to the chatbot.
/// </summary>
/// <param name="Message">The message to send to the chatbot.</param>
/// <param name="ConnectionId">The connection ID of the client.</param>
/// <param name="Streaming">Whether to stream the response.</param>
public record ChatQueryRequest
(
    string Message,
    string? ConnectionId = null,
    bool Streaming = false
) : IRequest<SingleResponse<ChatMessageDto?>>;