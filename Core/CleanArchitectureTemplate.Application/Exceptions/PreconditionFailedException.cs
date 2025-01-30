using System.Net;

namespace CleanArchitectureTemplate.Application.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a precondition for the request fails.
/// </summary>
public class PreconditionFailedException : Exception
{
    private const string DefaultMessage = "One or more preconditions for this request have failed.";

    /// <summary>
    /// Gets the error code associated with this exception.
    /// </summary>
    public string ErrorCode { get; } = "PRECONDITION_FAILED";

    /// <summary>
    /// Gets the HTTP status code associated with this exception.
    /// </summary>
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.PreconditionFailed;

    /// <summary>
    /// Initializes a new instance of the <see cref="PreconditionFailedException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error. Defaults to a standard message if null.</param>
    /// <param name="errorCode">A string-based error code (default is "PRECONDITION_FAILED").</param>
    /// <param name="innerException">The exception that caused the current exception, if any.</param>
    public PreconditionFailedException(string? message = DefaultMessage, string? errorCode = null, Exception? innerException = null)
        : base(message ?? DefaultMessage, innerException)
    {
        if (!string.IsNullOrWhiteSpace(errorCode))
            ErrorCode = errorCode;
    }
}