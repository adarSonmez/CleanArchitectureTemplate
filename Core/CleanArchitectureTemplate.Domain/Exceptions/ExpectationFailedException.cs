using System.Net;

namespace CleanArchitectureTemplate.Domain.Exceptions;

/// <summary>
/// Represents an exception that is thrown when an expectation in the request cannot be met.
/// </summary>
public class ExpectationFailedException : Exception
{
    private const string DefaultMessage = "The server cannot meet the expectations defined in the request.";

    /// <summary>
    /// Gets the error code associated with this exception.
    /// </summary>
    public string ErrorCode { get; } = "EXPECTATION_FAILED";

    /// <summary>
    /// Gets the HTTP status code associated with this exception.
    /// </summary>
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.ExpectationFailed;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpectationFailedException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error. Defaults to a standard message if null.</param>
    /// <param name="errorCode">A string-based error code (default is "EXPECTATION_FAILED").</param>
    /// <param name="innerException">The exception that caused the current exception, if any.</param>
    public ExpectationFailedException(string? message = DefaultMessage, string? errorCode = null, Exception? innerException = null)
        : base(message ?? DefaultMessage, innerException)
    {
        if (!string.IsNullOrWhiteSpace(errorCode))
            ErrorCode = errorCode;
    }
}