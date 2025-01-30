using System.Net;

namespace CleanArchitectureTemplate.Application.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a requested feature is not implemented.
/// </summary>
public class MissingImpementationException : Exception
{
    private const string DefaultMessage = "The requested feature is not implemented.";

    /// <summary>
    /// Gets the error code associated with this exception.
    /// </summary>
    public string ErrorCode { get; } = "NOT_IMPLEMENTED";

    /// <summary>
    /// Gets the HTTP status code associated with this exception.
    /// </summary>
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.NotImplemented;

    /// <summary>
    /// Initializes a new instance of the <see cref="MissingImpementationException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error. Defaults to a standard message if null.</param>
    /// <param name="errorCode">A string-based error code (default is "NOT_IMPLEMENTED").</param>
    /// <param name="innerException">The exception that caused the current exception, if any.</param>
    public MissingImpementationException(string? message = DefaultMessage, string? errorCode = null, Exception? innerException = null)
        : base(message ?? DefaultMessage, innerException)
    {
        if (!string.IsNullOrWhiteSpace(errorCode))
            ErrorCode = errorCode;
    }
}