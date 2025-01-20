using System.Net;

namespace CleanArchitectureTemplate.Domain.Exceptions;

/// <summary>
/// Represents an exception that is thrown when access to a resource is restricted due to legal reasons.
/// </summary>
public class UnavailableForLegalReasonsException : Exception
{
    private const string DefaultMessage = "Access to this resource is unavailable due to legal reasons.";

    /// <summary>
    /// Gets the error code associated with this exception.
    /// </summary>
    public string ErrorCode { get; } = "UNAVAILABLE_FOR_LEGAL_REASONS";

    /// <summary>
    /// Gets the HTTP status code associated with this exception.
    /// </summary>
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.UnavailableForLegalReasons;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnavailableForLegalReasonsException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error. Defaults to a standard message if null.</param>
    /// <param name="errorCode">A string-based error code (default is "UNAVAILABLE_FOR_LEGAL_REASONS").</param>
    /// <param name="innerException">The exception that caused the current exception, if any.</param>
    public UnavailableForLegalReasonsException(string? message = DefaultMessage, string? errorCode = null, Exception? innerException = null)
        : base(message ?? DefaultMessage, innerException)
    {
        if (!string.IsNullOrWhiteSpace(errorCode))
            ErrorCode = errorCode;
    }
}