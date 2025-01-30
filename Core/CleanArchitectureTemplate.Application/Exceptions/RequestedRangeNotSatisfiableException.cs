using System.Net;

namespace CleanArchitectureTemplate.Application.Exceptions;

/// <summary>
/// Represents an exception that is thrown when the requested range cannot be satisfied.
/// </summary>
public class RequestedRangeNotSatisfiableException : Exception
{
    private const string DefaultMessage = "The requested range cannot be satisfied.";

    /// <summary>
    /// Gets the error code associated with this exception.
    /// </summary>
    public string ErrorCode { get; } = "REQUESTED_RANGE_NOT_SATISFIABLE";

    /// <summary>
    /// Gets the HTTP status code associated with this exception.
    /// </summary>
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.RequestedRangeNotSatisfiable;

    /// <summary>
    /// Initializes a new instance of the <see cref="RequestedRangeNotSatisfiableException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error. Defaults to a standard message if null.</param>
    /// <param name="errorCode">A string-based error code (default is "REQUESTED_RANGE_NOT_SATISFIABLE").</param>
    /// <param name="innerException">The exception that caused the current exception, if any.</param>
    public RequestedRangeNotSatisfiableException(string? message = DefaultMessage, string? errorCode = null, Exception? innerException = null)
        : base(message ?? DefaultMessage, innerException)
    {
        if (!string.IsNullOrWhiteSpace(errorCode))
            ErrorCode = errorCode;
    }
}