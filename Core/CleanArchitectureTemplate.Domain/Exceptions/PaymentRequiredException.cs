using System.Net;

namespace CleanArchitectureTemplate.Domain.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a payment is required to access a resource or complete an operation.
/// </summary>
public class PaymentRequiredException : Exception
{
    private const string DefaultMessage = "Payment is required to access this resource.";

    /// <summary>
    /// Gets the error code associated with this exception.
    /// </summary>
    public string ErrorCode { get; } = "PAYMENT_REQUIRED";

    /// <summary>
    /// Gets the HTTP status code associated with this exception.
    /// </summary>
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.PaymentRequired;

    /// <summary>
    /// Initializes a new instance of the <see cref="PaymentRequiredException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error. Defaults to a standard message if null.</param>
    /// <param name="errorCode">A string-based error code (default is "PAYMENT_REQUIRED").</param>
    /// <param name="innerException">The exception that caused the current exception, if any.</param>
    public PaymentRequiredException(string? message = DefaultMessage, string? errorCode = null, Exception? innerException = null)
        : base(message ?? DefaultMessage, innerException)
    {
        if (!string.IsNullOrWhiteSpace(errorCode))
            ErrorCode = errorCode;
    }
}