using CleanArchitectureTemplate.Application.Abstractions.AI;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.AI;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.AI.Queries.Chat;

/// <summary>
/// Handles the <see cref="ChatQueryRequest"/>.
/// </summary>
public class ChatQueryHandler : IRequestHandler<ChatQueryRequest, SingleResponse<ChatMessageDto?>>
{
    private readonly IAIService _aiService;

    public ChatQueryHandler(IAIService aiService)
    {
        _aiService = aiService;
    }

    public async Task<SingleResponse<ChatMessageDto?>> Handle(ChatQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<ChatMessageDto?>();
        var chatMessage = await _aiService.SendMessageAsync(request.Message, request.ConnectionId, request.Streaming, cancellationToken);

        response.SetData(chatMessage);

        return response;
    }
}