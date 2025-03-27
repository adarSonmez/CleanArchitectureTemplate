using CleanArchitectureTemplate.AI.Cache;
using CleanArchitectureTemplate.Application.Abstractions.AI;
using CleanArchitectureTemplate.Application.Abstractions.Hubs;
using CleanArchitectureTemplate.Application.Dtos.AI;
using CleanArchitectureTemplate.Application.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.PromptTemplates.Handlebars;
using System.Text;

namespace CleanArchitectureTemplate.AI.Services;

/// <summary>
/// Service for interacting with the Semantic Kernel AI.
/// </summary>
public class SemanticKernelAIService : IAIService
{
    private readonly Kernel _kernel;
    private readonly ILogger<SemanticKernelAIService> _logger;
    private readonly IChatCompletionService _chatCompletionService;
    private readonly IAIHubService _chatHubService;
    private readonly IPromptTemplateFactory _templateFactory;
    private readonly string _promptDir;
    private readonly KernelFunction _detectLanguageFn;
    private readonly KernelFunction _translateFn;
    private readonly KernelFunction _summarizeFn;

    public SemanticKernelAIService(
        Kernel kernel,
        ILogger<SemanticKernelAIService> logger,
        IChatCompletionService chatCompletionService,
        IAIHubService chatHubService)
    {
        _kernel = kernel;
        _logger = logger;
        _chatCompletionService = chatCompletionService;
        _chatHubService = chatHubService;
        _templateFactory = new HandlebarsPromptTemplateFactory();
        _promptDir = Path.Combine(AppContext.BaseDirectory, "Prompts")!;

        KernelFunctionCache.Initialize(_kernel, _templateFactory, _promptDir, logger);
        _detectLanguageFn = KernelFunctionCache.DetectLanguageFn;
        _translateFn = KernelFunctionCache.TranslateFn;
        _summarizeFn = KernelFunctionCache.SummarizeFn;
    }

    /// <inheritdoc />
    public async Task<ChatMessageDto?> SendMessageAsync(string message, string? connectionId, bool streaming, CancellationToken cancellationToken)
    {
        return streaming
            ? await SendMessageStreamingAsync(message, connectionId, cancellationToken)
            : await SendMessageNonStreamingAsync(message, connectionId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<string?> DetectLanguage(string input)
    {
        var args = new KernelArguments()
        {
            { "input", input }
        };

        var result = await _kernel.InvokeAsync(_detectLanguageFn, args);
        return result.GetValue<string>();
    }

    /// <inheritdoc />
    public async Task<string?> Translate(string input, string language)
    {
        var args = new KernelArguments()
        {
            { "input", input },
            { "language", language }
        };
        var result = await _kernel.InvokeAsync(_translateFn, args);
        return result.GetValue<string>();
    }

    /// <inheritdoc />
    public async Task<string?> Summarize(string input)
    {
        var args = new KernelArguments()
        {
            { "input", input }
        };
        var result = await _kernel.InvokeAsync(_summarizeFn, args);
        return result.GetValue<string>();
    }

    #region Private Methods

    private async Task<ChatMessageDto> SendMessageStreamingAsync(string message, string? connectionId, CancellationToken cancellationToken)
    {
        const string ThinkStartTag = "<think>";
        const string ThinkEndTag = "</think>";

        var messageBuilder = new StringBuilder();
        var thinkBuilder = new StringBuilder();
        bool thinkCompleted = false;
        string? role = null;
        string? modelId = null;

        await foreach (var chunk in _chatCompletionService.GetStreamingChatMessageContentsAsync(message, cancellationToken: cancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            var currentMessage = chunk.Items.FirstOrDefault() as dynamic;
            if (currentMessage == null) continue;

            if (!thinkCompleted)
            {
                var thinkPart = currentMessage.Text?.ToString() ?? string.Empty;
                thinkBuilder.Append(thinkPart);
                if (thinkPart.Contains(ThinkEndTag))
                {
                    thinkCompleted = true;

                    if (thinkBuilder.Length >= ThinkStartTag.Length + ThinkEndTag.Length)
                    {
                        thinkBuilder.Remove(thinkBuilder.Length - ThinkEndTag.Length, ThinkEndTag.Length);
                        thinkBuilder.Remove(0, ThinkStartTag.Length);
                    }

                    role = chunk.Role?.ToString();
                    modelId = currentMessage.ModelId?.ToString();

                    continue;
                }
            }

            var currentText = currentMessage.Text?.ToString() ?? string.Empty;
            messageBuilder.Append(currentText);

            if (!string.IsNullOrEmpty(connectionId))
                await _chatHubService.AITypingAsync(messageBuilder.ToString(), string.Empty, true);
        }

        var chatMessageDto = new ChatMessageDto
        (
            role ?? string.Empty,
            thinkBuilder.ToString(),
            messageBuilder.ToString(),
            modelId ?? string.Empty,
            true
        );

        if (!string.IsNullOrEmpty(connectionId))
            await _chatHubService.SendAIResponseAsync(chatMessageDto, string.Empty);

        return chatMessageDto;
    }

    private async Task<ChatMessageDto> SendMessageNonStreamingAsync(string message, string? connectionId, CancellationToken cancellationToken)
    {
        const string ThinkStartTag = "<think>";
        const string ThinkEndTag = "</think>";

        var result = await _chatCompletionService.GetChatMessageContentAsync(message, cancellationToken: cancellationToken);
        var firstMessage = result.Items.FirstOrDefault() as dynamic
            ?? throw new OperationFailedException("No message content was returned.");

        var allMessage = firstMessage.Text?.ToString() ?? string.Empty;
        string? thinkContent = null;

        int startIndex = allMessage.IndexOf(ThinkStartTag, StringComparison.Ordinal);
        int endIndex = allMessage.IndexOf(ThinkEndTag, StringComparison.Ordinal);

        if (startIndex != -1 && endIndex != -1 && endIndex > startIndex)
        {
            startIndex += ThinkStartTag.Length;
            thinkContent = allMessage.Substring(startIndex, endIndex - startIndex);

            int removalStart = allMessage.IndexOf(ThinkStartTag, StringComparison.Ordinal);
            int removalLength = (endIndex - removalStart) + ThinkEndTag.Length;
            allMessage = allMessage.Remove(removalStart, removalLength);
        }

        var chatMessageDto = new ChatMessageDto
        (
            result.Role.ToString(),
            thinkContent,
            allMessage,
            firstMessage.ModelId?.ToString() ?? string.Empty,
            false
        );

        if (!string.IsNullOrEmpty(connectionId))
            await _chatHubService.SendAIResponseAsync(chatMessageDto, string.Empty);

        return chatMessageDto;
    }

    #endregion Private Methods
}