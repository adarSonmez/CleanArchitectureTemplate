namespace CleanArchitectureTemplate.AI.Plugins;

using Microsoft.SemanticKernel;
using System.ComponentModel;

/// <summary>
/// Plugin for text operations.
/// /// </summary>
public class TextPlugin
{
    [KernelFunction("count_words")]
    [Description("Counts the number of words in a string")]
    public int CountWords(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return 0;
        }
        return text.Split([' ', '\n', '\r'], StringSplitOptions.RemoveEmptyEntries).Length;
    }

    [KernelFunction("reverse_string")]
    [Description("Reverses a string")]
    public string ReverseString(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return string.Empty;
        }
        char[] charArray = text.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    [KernelFunction("to_uppercase")]
    [Description("Converts a string to uppercase")]
    public string ToUpperCase(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return string.Empty;
        }
        return text.ToUpper();
    }

    [KernelFunction("to_lowercase")]
    [Description("Converts a string to lowercase")]
    public string ToLowerCase(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return string.Empty;
        }
        return text.ToLower();
    }
}