using AutoMapper;
using CleanArchitectureTemplate.Application.Abstractions.AI;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.AI;
using CleanArchitectureTemplate.Application.Features.AI.Queries.Chat;
using MediatR;

namespace CleanArchitectureTemplate.AI.Features.Chat.Queries.Chat;

/// <summary>
/// Handles the <see cref="ChatQueryRequest"/>.
/// </summary>
public class ChatQueryHandler : IRequestHandler<ChatQueryRequest, SingleResponse<ChatMessageDto?>>
{
    private readonly IMapper _mapper;
    private readonly IAiService _aiService;

    public ChatQueryHandler(IMapper mapper, IAiService aiService)
    {
        _mapper = mapper;
        _aiService = aiService;
    }

    public async Task<SingleResponse<ChatMessageDto?>> Handle(ChatQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<ChatMessageDto?>();

        var chatMessage = await _aiService.SendMessageAsync(request.Message);

        response.SetData(chatMessage);

        return response;
    }
}