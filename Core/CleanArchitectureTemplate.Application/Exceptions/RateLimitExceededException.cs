using System.Net;

namespace CleanArchitectureTemplate.Application.Exceptions;

/// <summary>
/// Represents an exception that is thrown when the client exceeds the allowed rate limit for requests.
/// </summary>
public class RateLimitExceededException : Exception
{
    private const string DefaultMessage = "The rate limit has been exceeded. Please try again later.";

    /// <summary>
    /// Gets the error code associated with this exception.
    /// </summary>
    public string ErrorCode { get; } = "RATE_LIMIT_EXCEEDED";

    /// <summary>
    /// Gets the HTTP status code associated with this exception.
    /// </summary>
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.TooManyRequests;

    /// <summary>
    /// Initializes a new instance of the <see cref="RateLimitExceededException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error. Defaults to a standard message if null.</param>
    /// <param name="errorCode">A string-based error code (default is "RATE_LIMIT_EXCEEDED").</param>
    /// <param name="innerException">The exception that caused the current exception, if any.</param>
    public RateLimitExceededException(string? message = DefaultMessage, string? errorCode = null, Exception? innerException = null)
        : base(message ?? DefaultMessage, innerException)
    {
        if (!string.IsNullOrWhiteSpace(errorCode))
            ErrorCode = errorCode;
    }
}