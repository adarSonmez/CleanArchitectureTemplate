using CleanArchitectureTemplate.Application.Dtos.AI;

namespace CleanArchitectureTemplate.Application.Abstractions.AI;

/// <summary>
/// Central AI service abstraction.
/// </summary>
public interface IAiService
{
    /// <summary>
    /// Sends a message to the AI service and receives a response.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <returns>The response from the AI service.</returns>
    Task<ChatMessageDto?> SendMessageAsync(string message);

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