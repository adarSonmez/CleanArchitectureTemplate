using System.Net;

namespace CleanArchitectureTemplate.Application.Exceptions;

/// <summary>
/// Represents an exception that is thrown when an operation fails unexpectedly.
/// </summary>
public class OperationFailedException : Exception
{
    private const string DefaultMessage = "The operation failed to complete successfully.";

    /// <summary>
    /// Gets the error code associated with this exception.
    /// </summary>
    public string ErrorCode { get; } = "OPERATION_FAILED";

    /// <summary>
    /// Gets the HTTP status code associated with this exception.
    /// </summary>
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.InternalServerError;

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationFailedException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error. Defaults to a standard message if null.</param>
    /// <param name="errorCode">A string-based error code (default is "OPERATION_FAILED").</param>
    /// <param name="innerException">The exception that caused the current exception, if any.</param>
    public OperationFailedException(string? message = DefaultMessage, string? errorCode = null, Exception? innerException = null)
        : base(message ?? DefaultMessage, innerException)
    {
        if (!string.IsNullOrWhiteSpace(errorCode))
            ErrorCode = errorCode;
    }
}