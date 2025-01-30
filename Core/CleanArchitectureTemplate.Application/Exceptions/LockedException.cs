using System.Net;

namespace CleanArchitectureTemplate.Application.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a resource is locked and cannot be modified.
/// </summary>
public class LockedException : Exception
{
    private const string DefaultMessage = "The resource is locked and cannot be modified.";

    /// <summary>
    /// Gets the error code associated with this exception.
    /// </summary>
    public string ErrorCode { get; } = "RESOURCE_LOCKED";

    /// <summary>
    /// Gets the HTTP status code associated with this exception.
    /// </summary>
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.Locked;

    /// <summary>
    /// Initializes a new instance of the <see cref="LockedException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error. Defaults to a standard message if null.</param>
    /// <param name="errorCode">A string-based error code (default is "RESOURCE_LOCKED").</param>
    /// <param name="innerException">The exception that caused the current exception, if any.</param>
    public LockedException(string? message = DefaultMessage, string? errorCode = null, Exception? innerException = null)
        : base(message ?? DefaultMessage, innerException)
    {
        if (!string.IsNullOrWhiteSpace(errorCode))
            ErrorCode = errorCode;
    }
}