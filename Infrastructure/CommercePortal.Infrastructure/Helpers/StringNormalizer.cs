using System.Text.RegularExpressions;

namespace CommercePortal.Infrastructure.Helpers;

/// <summary>
/// Utility class for normalizing strings for file, directory, URL, and other naming purposes.
/// </summary>
public static class StringNormalizer
{
    #region Public Methods

    /// <summary>
    /// Normalizes a string to create a valid file name.
    /// </summary>
    public static string NormalizeFileName(string input, char replacement = '_') =>
        Normalize(input, Path.GetInvalidFileNameChars(), replacement);

    /// <summary>
    /// Normalizes a string to create a valid directory name.
    /// </summary>
    public static string NormalizeDirectoryName(string input, char replacement = '_') =>
        Normalize(input, Path.GetInvalidPathChars(), replacement);

    /// <summary>
    /// Normalizes a string for use as a URL by replacing invalid URL characters.
    /// </summary>
    /// <param name="input">The input string to normalize.</param>
    /// <param name="replacement">The character to replace invalid characters with (default is '-').</param>
    /// <returns>A string suitable for use in a URL.</returns>
    public static string NormalizeForUrl(string input, char replacement = '-')
    {
        // Define invalid characters for URLs based on common conventions.
        var invalidChars = new[] { ' ', '/', '\\', '?', '#', '%', '&', '*', ':', '<', '>', '"', '|' };
        return Normalize(input, invalidChars, replacement);
    }

    /// <summary>
    /// Removes all whitespace from a string.
    /// </summary>
    /// <param name="input">The input string to process.</param>
    /// <returns>A string with all whitespace removed.</returns>
    public static string RemoveWhitespace(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Input cannot be null or empty", nameof(input));
        }

        return Regex.Replace(input, @"\s+", "");
    }

    /// <summary>
    /// Converts a string to a safe slug for URLs or file names.
    /// </summary>
    /// <param name="input">The input string to slugify.</param>
    /// <returns>A slugified version of the string.</returns>
    public static string Slugify(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Input cannot be null or empty", nameof(input));
        }

        // Convert to lowercase, replace spaces with hyphens, and remove invalid characters.
        input = input.ToLowerInvariant();
        input = Regex.Replace(input, @"\s+", "-"); // Replace spaces with hyphens.
        input = Regex.Replace(input, @"[^a-z0-9\-]", ""); // Remove non-alphanumeric characters.
        return input.Trim('-');
    }

    /// <summary>
    /// Trims a string to a specified maximum length.
    /// </summary>
    /// <param name="input">The input string to trim.</param>
    /// <param name="maxLength">The maximum length of the resulting string.</param>
    /// <returns>The trimmed string.</returns>
    public static string TrimToMaxLength(string input, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Input cannot be null or empty", nameof(input));
        }

        if (maxLength < 0)
        {
            throw new ArgumentException("Maximum length must be non-negative", nameof(maxLength));
        }

        return input.Length > maxLength ? input[..maxLength] : input;
    }

    /// <summary>
    /// Converts a string to PascalCase.
    /// </summary>
    /// <param name="input">The input string to convert.</param>
    /// <returns>The string in PascalCase format.</returns>
    public static string ToPascalCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Input cannot be null or empty", nameof(input));
        }

        return Regex.Replace(input, @"(\b[a-z])", match => match.Value.ToUpperInvariant())
                    .Replace(" ", "");
    }

    /// <summary>
    /// Converts a string to camelCase.
    /// </summary>
    /// <param name="input">The input string to convert.</param>
    /// <returns>The string in camelCase format.</returns>
    public static string ToCamelCase(string input)
    {
        var pascalCase = ToPascalCase(input);
        return string.IsNullOrEmpty(pascalCase) ? pascalCase : char.ToLowerInvariant(pascalCase[0]) + pascalCase.Substring(1);
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Normalizes a string for use by replacing invalid characters.
    /// </summary>
    /// <param name="input">The input string to normalize.</param>
    /// <param name="invalidChars">Array of characters to replace.</param>
    /// <param name="replacement">The character to replace invalid characters with (default is '_').</param>
    /// <returns>A normalized string with invalid characters replaced.</returns>
    /// <exception cref="ArgumentException">Thrown when the input is null or empty.</exception>
    private static string Normalize(string input, char[] invalidChars, char replacement)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException("Input cannot be null or empty", nameof(input));
        }

        var pattern = $"[{Regex.Escape(new string(invalidChars))}]";
        return Regex.Replace(input, pattern, replacement.ToString()).Trim();
    }

    #endregion Private Methods
}