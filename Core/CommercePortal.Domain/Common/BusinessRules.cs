using CommercePortal.Domain.Constants.StringConstants;
using CommercePortal.Domain.Exceptions;
using CommercePortal.Domain.Extensions;
using CommercePortal.Domain.MarkerInterfaces;

namespace CommercePortal.Domain.Common;

/// <summary>
/// Contains utility methods for performing common business rules checks.
/// </summary>
public static class BusinessRules
{
    /// <summary>
    /// Runs the specified business rules checks and throws a ValidationException if any check fails.
    /// </summary>
    /// <param name="checkResults">The results of the business rules checks.</param>
    public static void Run(params (string? errCode, string msg)[] checkResults)
    {
        foreach (var (errCode, msg) in checkResults)
        {
            if (errCode is not null)
                throw new BusinessRuleViolationException(msg, errCode);
        }
    }

    #region Validation Checks

    /// <summary>
    /// Checks if the specified entity object is null and returns an error message if it is.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="obj">The entity object to check.</param>
    /// <param name="customError">The custom error message to return if the entity is null.</param>
    /// <returns>The error message if the entity is null; otherwise, null.</returns>
    public static string? CheckEntityNull<TEntity>(TEntity? obj, string? customError = null)
        where TEntity : IEntity
    {
        if (obj is not null)
            return null;

        customError ??= $"{typeof(TEntity).Name} Not Found!";

        return customError;
    }

    /// <summary>
    /// Checks if the specified DTO object is null and returns an error message if it is.
    /// </summary>
    /// <typeparam name="TDto">The type of the DTO.</typeparam>
    /// <param name="obj">The DTO object to check.</param>
    /// <param name="customError">The custom error message to return if the DTO is null.</param>
    /// <returns>The error message if the DTO is null; otherwise, null.</returns>
    public static string? CheckDtoNull<TDto>(TDto? obj, string customError = BusinessRuleMessages.NullObjectPassed)
        where TDto : IDto
    {
        if (obj is not null)
            return null;

        return customError;
    }

    /// <summary>
    /// Checks if the specified email address is valid.
    /// </summary>
    /// <param name="email">The email address to check.</param>
    /// <returns>The error message if the email address is not valid; otherwise, null.</returns>
    public static string? CheckEmail(string email)
    {
        return !email.IsValidEmail() ? BusinessRuleMessages.EmailFormatIsNotValid : null;
    }

    /// <summary>
    /// Checks if the specified GUID is valid.
    /// </summary>
    /// <param name="guid">The GUID to check.</param>
    /// <returns>The error message if the GUID is not valid; otherwise, null.</returns>
    public static string? CheckGuid(string guid)
    {
        return !guid.IsValidGuid() ? BusinessRuleMessages.GuidFormatIsNotValid : null;
    }

    /// <summary>
    /// Checks if the specified string is null or empty.
    /// </summary>
    /// <param name="str">The string to check.</param>
    /// <param name="customError">The custom error message to return if the string is null or empty.</param>
    /// <returns>The error message if the string is null or empty; otherwise, null.</returns>
    public static string? CheckStringNullOrEmpty(string? str, string customError = BusinessRuleMessages.StringCannotBeNullOrEmpty)
    {
        if (string.IsNullOrEmpty(str))
            return customError;

        return null;
    }

    #endregion Validation Checks
}