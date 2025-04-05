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

namespace CleanArchitectureTemplate.AI.Services
{
    /// <summary>
    /// Service for interacting with the Semantic Kernel AI.
    /// </summary>
    public class SemanticKernelAIService : IAIService
    {
        private readonly Kernel _kernel;
        private readonly ILogger<SemanticKernelAIService> _logger;
        private readonly IChatCompletionService _chatCompletionService;
        private readonly IAIHubService _chatHubService;
        private readonly IChatHistoryService _chatHistoryService;
        private readonly IPromptTemplateFactory _templateFactory;
        private readonly string _promptDir;
        private readonly KernelFunction _detectLanguageFn;
        private readonly KernelFunction _translateFn;
        private readonly KernelFunction _summarizeFn;
        private readonly PromptExecutionSettings _promptExecutionSettings;

        public SemanticKernelAIService(
            Kernel kernel,
            ILogger<SemanticKernelAIService> logger,
            IChatCompletionService chatCompletionService,
            IAIHubService chatHubService,
            IChatHistoryService chatHistoryService)
        {
            _kernel = kernel;
            _logger = logger;
            _chatCompletionService = chatCompletionService;
            _chatHubService = chatHubService;
            _chatHistoryService = chatHistoryService;
            _templateFactory = new HandlebarsPromptTemplateFactory();
            _promptDir = Path.Combine(AppContext.BaseDirectory, "Prompts")!;
            _promptExecutionSettings = new PromptExecutionSettings
            {
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
            };

            KernelFunctionCache.Initialize(_kernel, _templateFactory, _promptDir, _logger);
            _detectLanguageFn = KernelFunctionCache.DetectLanguageFn;
            _translateFn = KernelFunctionCache.TranslateFn;
            _summarizeFn = KernelFunctionCache.SummarizeFn;
        }

        /// <inheritdoc />
        public async Task<ChatMessageDto?> SendMessageAsync(string message, string connectionId, bool streaming, CancellationToken cancellationToken)
        {
            _chatHistoryService.AddMessage(connectionId, AuthorRole.User, message);

            var chatMessageDto = streaming
                ? await SendMessageStreamingAsync(connectionId, cancellationToken)
                : await SendMessageNonStreamingAsync(connectionId, cancellationToken);

            if (chatMessageDto is not null && !string.IsNullOrEmpty(connectionId))
            {
                _chatHistoryService.AddMessage(connectionId, AuthorRole.Assistant, chatMessageDto.Content ?? string.Empty);
            }

            return chatMessageDto;
        }

        /// <inheritdoc />
        public async Task<string?> DetectLanguage(string input)
        {
            var args = new KernelArguments { { "input", input } };
            var result = await _kernel.InvokeAsync(_detectLanguageFn, args);
            return result.GetValue<string>();
        }

        /// <inheritdoc />
        public async Task<string?> Translate(string input, string language)
        {
            var args = new KernelArguments
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
            var args = new KernelArguments { { "input", input } };
            var result = await _kernel.InvokeAsync(_summarizeFn, args);
            return result.GetValue<string>();
        }

        #region Private Methods

        private async Task<ChatMessageDto> SendMessageStreamingAsync(string connectionId, CancellationToken cancellationToken)
        {
            var chatHistory = _chatHistoryService.GetHistory(connectionId) as ChatHistory
                              ?? throw new ForbiddenException("Chat history not found.");

            var messageBuilder = new StringBuilder();
            var thinkBuilder = new StringBuilder();
            string? role = null;
            string? modelId = null;

            await foreach (var chunk in _chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory, _promptExecutionSettings, _kernel, cancellationToken))
            {
                cancellationToken.ThrowIfCancellationRequested();

                messageBuilder.Append(chunk.Content);

                if (role is null || modelId is null)
                {
                    role = chunk.Role.ToString();
                    modelId = chunk.ModelId;
                }

                if (!string.IsNullOrEmpty(connectionId))
                {
                    await _chatHubService.AITypingAsync(messageBuilder.ToString(), string.Empty, true);
                }
            }

            var chatMessageDto = new ChatMessageDto(
                role ?? string.Empty,
                thinkBuilder.ToString(),
                messageBuilder.ToString(),
                modelId ?? string.Empty,
                true);

            if (!string.IsNullOrEmpty(connectionId))
            {
                await _chatHubService.SendAIResponseAsync(chatMessageDto, string.Empty);
            }

            return chatMessageDto;
        }

        private async Task<ChatMessageDto> SendMessageNonStreamingAsync(string connectionId, CancellationToken cancellationToken)
        {
            var chatHistory = _chatHistoryService.GetHistory(connectionId) as ChatHistory
                              ?? throw new ForbiddenException("Chat history not found.");

            var result = await _chatCompletionService.GetChatMessageContentAsync(chatHistory, _promptExecutionSettings, _kernel, cancellationToken);

            var chatMessageDto = new ChatMessageDto(
                result.Role.ToString(),
                string.Empty,
                result.Content,
                result.ModelId,
                false);

            if (!string.IsNullOrEmpty(connectionId))
            {
                await _chatHubService.SendAIResponseAsync(chatMessageDto, string.Empty);
            }

            return chatMessageDto;
        }

        #endregion Private Methods
    }
}