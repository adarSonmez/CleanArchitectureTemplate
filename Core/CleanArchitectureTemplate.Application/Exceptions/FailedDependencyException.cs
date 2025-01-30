using System.Net;

namespace CleanArchitectureTemplate.Application.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a dependency fails, causing the operation to fail.
/// </summary>
public class FailedDependencyException : Exception
{
    private const string DefaultMessage = "A required dependency has failed, causing the operation to fail.";

    /// <summary>
    /// Gets the error code associated with this exception.
    /// </summary>
    public string ErrorCode { get; } = "FAILED_DEPENDENCY";

    /// <summary>
    /// Gets the HTTP status code associated with this exception.
    /// </summary>
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.FailedDependency;

    /// <summary>
    /// Initializes a new instance of the <see cref="FailedDependencyException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error. Defaults to a standard message if null.</param>
    /// <param name="errorCode">A string-based error code (default is "FAILED_DEPENDENCY").</param>
    /// <param name="innerException">The exception that caused the current exception, if any.</param>
    public FailedDependencyException(string? message = DefaultMessage, string? errorCode = null, Exception? innerException = null)
        : base(message ?? DefaultMessage, innerException)
    {
        if (!string.IsNullOrWhiteSpace(errorCode))
            ErrorCode = errorCode;
    }
}