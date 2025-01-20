using System.Net;

namespace CleanArchitectureTemplate.Domain.Exceptions;

/// <summary>
/// Represents an exception that is thrown when the client exceeds the rate limit for requests.
/// </summary>
public class TooManyRequestsException : Exception
{
    private const string DefaultMessage = "Too many requests have been made. Please try again later.";

    /// <summary>
    /// Gets the error code associated with this exception.
    /// </summary>
    public string ErrorCode { get; } = "TOO_MANY_REQUESTS";

    /// <summary>
    /// Gets the HTTP status code associated with this exception.
    /// </summary>
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.TooManyRequests;

    /// <summary>
    /// Initializes a new instance of the <see cref="TooManyRequestsException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error. Defaults to a standard message if null.</param>
    /// <param name="errorCode">A string-based error code (default is "TOO_MANY_REQUESTS").</param>
    /// <param name="innerException">The exception that caused the current exception, if any.</param>
    public TooManyRequestsException(string? message = DefaultMessage, string? errorCode = null, Exception? innerException = null)
        : base(message ?? DefaultMessage, innerException)
    {
        if (!string.IsNullOrWhiteSpace(errorCode))
            ErrorCode = errorCode;
    }
}