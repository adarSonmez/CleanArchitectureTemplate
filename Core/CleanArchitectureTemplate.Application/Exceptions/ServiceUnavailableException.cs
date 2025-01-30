using System.Net;

namespace CleanArchitectureTemplate.Application.Exceptions;

/// <summary>
/// Represents an exception that is thrown when the service is unavailable.
/// </summary>
public class ServiceUnavailableException : Exception
{
    private const string DefaultMessage = "The service is temporarily unavailable.";

    /// <summary>
    /// Gets the error code associated with this exception.
    /// </summary>
    public string ErrorCode { get; } = "SERVICE_UNAVAILABLE";

    /// <summary>
    /// Gets the HTTP status code associated with this exception.
    /// </summary>
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.ServiceUnavailable;

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceUnavailableException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error. Defaults to a standard message if null.</param>
    /// <param name="errorCode">A string-based error code (default is "SERVICE_UNAVAILABLE").</param>
    /// <param name="innerException">The exception that caused the current exception, if any.</param>
    public ServiceUnavailableException(string? message = DefaultMessage, string? errorCode = null, Exception? innerException = null)
        : base(message ?? DefaultMessage, innerException)
    {
        if (!string.IsNullOrWhiteSpace(errorCode))
            ErrorCode = errorCode;
    }
}