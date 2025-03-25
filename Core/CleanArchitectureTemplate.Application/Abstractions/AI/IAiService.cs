using CleanArchitectureTemplate.Application.Dtos.AI;

namespace CleanArchitectureTemplate.Application.Abstractions.AI;

/// <summary>
/// Central AI service abstraction.
/// </summary>
public interface IAIService
{
    /// <summary>
    /// Sends a message to the AI service and receives a response.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="connectionId">The connection ID of the client.</param>
    /// <param name="streaming">Whether to stream the response.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The response from the AI service.</returns>
    Task<ChatMessageDto?> SendMessageAsync(string message, string? connectionId, bool streaming, CancellationToken cancellationToken);

    /// <summary>
    /// Detects the language of the input text.
    /// </summary>
    /// <param name="input">The input text.</param>
    /// <returns>The detected language.</returns>
    Task<string?> DetectLanguage(string input);

    /// <summary>
    /// Summarizes the input text.
    /// </summary>
    /// <param name="input">The input text.</param>
    Task<string?> Summarize(string input);

    /// <summary>
    /// Translates the input text to the target language.
    /// </summary>
    /// <param name="input">The input text.</param>
    /// <param name="targetLanguage">The target language.</param>
    /// <returns>The translated text.</returns>
    Task<string?> Translate(string input, string targetLanguage);
}