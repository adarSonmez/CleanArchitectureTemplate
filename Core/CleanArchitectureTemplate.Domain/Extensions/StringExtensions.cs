using System.Net.Mail;

namespace CleanArchitectureTemplate.Domain.Extensions;

/// <summary>
/// Provides extension methods for string manipulation and validation.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Determines whether the specified string is a valid email address.
    /// </summary>
    /// <param name="email">The string to validate as an email address.</param>
    /// <returns><c>true</c> if the string is a valid email address; otherwise, <c>false</c>.</returns>
    public static bool IsValidEmail(this string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Determines whether the specified string is a valid GUID.
    /// </summary>
    /// <param name="guid">The string to validate as a GUID.</param>
    /// <returns><c>true</c> if the string is a valid GUID; otherwise, <c>false</c>.</returns>
    public static bool IsValidGuid(this string guid)
    {
        return Guid.TryParse(guid, out _);
    }
}