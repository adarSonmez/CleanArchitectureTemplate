using CleanArchitectureTemplate.Application.Exceptions;
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace CleanArchitectureTemplate.AI.Plugins;

/// <summary>
/// Plugin for date and time operations.
/// </summary>
public class DateTimePlugin
{
    [KernelFunction("get_utc_now")]
    [Description("Gets the current UTC timestamp")]
    [return: Description("Current UTC timestamp in 'u' format")]
    public string GetUtcNow() => DateTime.UtcNow.ToString("u");

    [KernelFunction("get_day_of_week")]
    [Description("Gets the current day of the week")]
    [return: Description("The name of the current day of the week")]
    public string GetDayOfWeek() => DateTime.UtcNow.DayOfWeek.ToString();

    [KernelFunction("add_days")]
    [Description("Adds a number of days to the current date")]
    [return: Description("New date in 'u' format after adding the specified number of days")]
    public string AddDays(int days)
    {
        var newDate = DateTime.UtcNow.AddDays(days);
        return newDate.ToString("u");
    }

    [KernelFunction("subtract_days")]
    [Description("Subtracts a number of days from the current date")]
    [return: Description("New date in 'u' format after subtracting the specified number of days")]
    public string SubtractDays(int days)
    {
        var newDate = DateTime.UtcNow.AddDays(-days);
        return newDate.ToString("u");
    }

    [KernelFunction("format_date")]
    [Description("Formats a date string to a specified format")]
    [return: Description("Formatted date string")]
    public string FormatDate(string date, string format)
    {
        if (DateTime.TryParse(date, out var parsedDate))
        {
            return parsedDate.ToString(format);
        }
        throw new ValidationFailedException("Invalid date format");
    }
}