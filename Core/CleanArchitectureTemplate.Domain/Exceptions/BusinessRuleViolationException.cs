namespace CleanArchitectureTemplate.Domain.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a business rule is violated.
/// </summary>
public class BusinessRuleViolationException : Exception
{
    #region Private Constants

    private const string _defaultErrorPrefix = "VAL";
    private const string _defaultErrorNumber = "000000";

    #endregion Private Constants

    #region Public Properties

    /// <summary>
    /// Gets the name of the business rule that was violated.
    /// </summary>
    public string ErrorCode { get; } = _defaultErrorPrefix + _defaultErrorNumber;

    #endregion Public Properties

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessRuleViolationException"/> class.
    /// </summary>
    public BusinessRuleViolationException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessRuleViolationException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public BusinessRuleViolationException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessRuleViolationException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public BusinessRuleViolationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessRuleViolationException"/> class with a specified error message and the name of the business rule that was violated.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="errorCode">6-digit numeric error code representing the business rule that was violated.</param>
    /// <param name="errorPrefix">The prefix to use for the error code.</param>
    public BusinessRuleViolationException(string message, string? errorCode, string errorPrefix = _defaultErrorPrefix)
        : base(message)
    {
        if (errorCode is not null && (errorCode.Length != 6 || !int.TryParse(errorCode, out _)))
            throw new ArgumentException("The error code must be a 6-digit numeric value.");

        ErrorCode = errorCode ?? errorPrefix + _defaultErrorNumber;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessRuleViolationException"/> class with a specified error message, the name of the business rule that was violated, and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="errorCode">6-digit numeric error code representing the business rule that was violated.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    /// <param name="errorPrefix">The prefix to use for the error code.</param>
    public BusinessRuleViolationException(string message, Exception innerException, string? errorCode, string errorPrefix = _defaultErrorPrefix)
        : base(message, innerException)
    {
        if (errorCode is not null && (errorCode.Length != 6 || !int.TryParse(errorCode, out _)))
            throw new ArgumentException("The error code must be a 6-digit numeric value.");

        ErrorCode = errorCode ?? errorPrefix + _defaultErrorNumber;
    }

    #endregion Constructors
}