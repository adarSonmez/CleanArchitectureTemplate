using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.AI;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.AI.Queries.Chat;

/// <summary>
/// Request to send a message to the chatbot.
/// </summary>
/// <param name="Message">The message to send to the chatbot.</param>
public record ChatQueryRequest
(
    string Message
) : IRequest<SingleResponse<ChatMessageDto?>>;