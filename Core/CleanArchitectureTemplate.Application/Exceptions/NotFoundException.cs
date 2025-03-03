using System.Net;

namespace CleanArchitectureTemplate.Application.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a requested resource is not found.
/// </summary>
public class NotFoundException : Exception
{
    private const string DefaultMessage = "The requested resource was not found.";

    /// <summary>
    /// Gets the error code associated with this exception.
    /// </summary>
    public string ErrorCode { get; } = "RESOURCE_NOT_FOUND";

    /// <summary>
    /// Gets the HTTP status code associated with this exception.
    /// </summary>
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.NotFound;

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error. Defaults to a standard message if null.</param>
    /// <param name="errorCode">A string-based error code (default is "RESOURCE_NOT_FOUND").</param>
    /// <param name="innerException">The exception that caused the current exception, if any.</param>
    public NotFoundException(string? message = DefaultMessage, string? errorCode = null, Exception? innerException = null)
        : base(message ?? DefaultMessage, innerException)
    {
        if (!string.IsNullOrWhiteSpace(errorCode))
            ErrorCode = errorCode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class for a specific resource and identifier.
    /// </summary>
    /// <param name="resourceName">The name of the resource that was not found.</param>
    /// <param name="identifier">The identifier of the resource that was not found.</param>
    public NotFoundException(string resourceName, Guid identifier)
        : base($"The resource '{resourceName}' with identifier '{identifier}' was not found.")
    {
    }
}