using CommercePortal.Application.Common.Constants.Enums;

namespace CommercePortal.Application.Common.Responses;

/// <summary>
/// Represents a response message with a type and optional code.
/// </summary>
public record ResponseMessage(string Message, ResponseMessageType MessageType, string? Code = null)
{
    /// <summary>
    /// Creates a new success message.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns>The success message.</returns>
    public static ResponseMessage Success(string message) =>
        new(message, ResponseMessageType.Success);

    /// <summary>
    /// Creates a new warning message.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns>The warning message.</returns>
    public static ResponseMessage Warning(string message) =>
        new(message, ResponseMessageType.Warning);

    /// <summary>
    /// Creates a new error message.
    /// </summary>
    /// <param name="code">The code.</param>
    /// <param name="message">The message.</param>
    /// <returns>The error message.</returns>
    public static ResponseMessage Error(string code, string message) =>
        new(message, ResponseMessageType.Error, code);
}