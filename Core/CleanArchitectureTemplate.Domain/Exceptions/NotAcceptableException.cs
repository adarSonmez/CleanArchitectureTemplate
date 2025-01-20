using System.Net;

namespace CleanArchitectureTemplate.Domain.Exceptions;

/// <summary>
/// Represents an exception that is thrown when the server cannot produce a response matching the request's Accept headers.
/// </summary>
public class NotAcceptableException : Exception
{
    private const string DefaultMessage = "The requested resource is not available in an acceptable format.";

    /// <summary>
    /// Gets the error code associated with this exception.
    /// </summary>
    public string ErrorCode { get; } = "NOT_ACCEPTABLE";

    /// <summary>
    /// Gets the HTTP status code associated with this exception.
    /// </summary>
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.NotAcceptable;

    /// <summary>
    /// Initializes a new instance of the <see cref="NotAcceptableException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error. Defaults to a standard message if null.</param>
    /// <param name="errorCode">A string-based error code (default is "NOT_ACCEPTABLE").</param>
    /// <param name="innerException">The exception that caused the current exception, if any.</param>
    public NotAcceptableException(string? message = DefaultMessage, string? errorCode = null, Exception? innerException = null)
        : base(message ?? DefaultMessage, innerException)
    {
        if (!string.IsNullOrWhiteSpace(errorCode))
            ErrorCode = errorCode;
    }
}