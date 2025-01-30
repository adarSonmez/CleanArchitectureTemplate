using System.Net;

namespace CleanArchitectureTemplate.Application.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a request times out.
/// </summary>
public class RequestTimeoutException : Exception
{
    private const string DefaultMessage = "The request timed out.";

    /// <summary>
    /// Gets the error code associated with this exception.
    /// </summary>
    public string ErrorCode { get; } = "REQUEST_TIMEOUT";

    /// <summary>
    /// Gets the HTTP status code associated with this exception.
    /// </summary>
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.RequestTimeout;

    /// <summary>
    /// Initializes a new instance of the <see cref="RequestTimeoutException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error. Defaults to a standard message if null.</param>
    /// <param name="errorCode">A string-based error code (default is "REQUEST_TIMEOUT").</param>
    /// <param name="innerException">The exception that caused the current exception, if any.</param>
    public RequestTimeoutException(string? message = DefaultMessage, string? errorCode = null, Exception? innerException = null)
        : base(message ?? DefaultMessage, innerException)
    {
        if (!string.IsNullOrWhiteSpace(errorCode))
            ErrorCode = errorCode;
    }
}