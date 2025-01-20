using System.Net;

namespace CleanArchitectureTemplate.Domain.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a conflict occurs with the current state of a resource.
/// </summary>
public class ConflictException : Exception
{
    private const string DefaultMessage = "A conflict occurred with the current state of the resource.";

    /// <summary>
    /// Gets the error code associated with this exception.
    /// </summary>
    public string ErrorCode { get; } = "CONFLICT";

    /// <summary>
    /// Gets the HTTP status code associated with this exception.
    /// </summary>
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.Conflict;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConflictException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error. Defaults to a standard message if null.</param>
    /// <param name="errorCode">A string-based error code (default is "CONFLICT").</param>
    /// <param name="innerException">The exception that caused the current exception, if any.</param>
    public ConflictException(string? message = DefaultMessage, string? errorCode = null, Exception? innerException = null)
        : base(message ?? DefaultMessage, innerException)
    {
        if (!string.IsNullOrWhiteSpace(errorCode))
            ErrorCode = errorCode;
    }
}