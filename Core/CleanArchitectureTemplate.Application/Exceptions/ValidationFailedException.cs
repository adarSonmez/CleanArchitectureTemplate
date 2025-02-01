using System.Net;

namespace CleanArchitectureTemplate.Application.Exceptions;

/// <summary>
/// Represents an exception that is thrown when validation of input fails.
/// </summary>
public class ValidationFailedException : Exception
{
    private const string DefaultMessage = "Validation of the input failed.";

    /// <summary>
    /// Gets the error code associated with this exception.
    /// </summary>
    public string ErrorCode { get; } = "VALIDATION_ERROR";

    /// <summary>
    /// Gets the HTTP status code associated with this validation error.
    /// </summary>
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.UnprocessableEntity;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationFailedException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the validation error. Defaults to a standard message if null.</param>
    /// <param name="errorCode">A string-based error code (default is "VALIDATION_ERROR").</param>
    /// <param name="innerException">The exception that caused the current exception, if any.</param>
    public ValidationFailedException(string? message = DefaultMessage, string? errorCode = null, Exception? innerException = null)
        : base(message ?? DefaultMessage, innerException)
    {
        if (!string.IsNullOrWhiteSpace(errorCode))
            ErrorCode = errorCode;
    }
}