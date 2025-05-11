namespace CleanArchitectureTemplate.Application.Common.Responses;

/// <summary>
/// Represents a response message with a type and optional code.
/// </summary>
public record ResponseMessage(string Message, string MessageType, string? ErrorCode = null)
{
    /// <summary>
    /// Creates a new success message.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns>The success message.</returns>
    public static ResponseMessage Success(string message) =>
        new(message, ResponseConstants.Success);

    /// <summary>
    /// Creates a new warning message.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns>The warning message.</returns>
    public static ResponseMessage Warning(string message) =>
        new(message, ResponseConstants.Warning);

    /// <summary>
    /// Creates a new error message.
    /// </summary>
    /// <param name="code">The code.</param>
    /// <param name="message">The message.</param>
    /// <returns>The error message.</returns>
    public static ResponseMessage Error(string code, string message) =>
        new(message, ResponseConstants.Error, code);
}