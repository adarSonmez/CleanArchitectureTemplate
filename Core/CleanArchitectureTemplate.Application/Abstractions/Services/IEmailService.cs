namespace CleanArchitectureTemplate.Application.Abstractions.Services;

/// <summary>
/// Represents a service for sending and receiving emails.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends an email asynchronously.
    /// </summary>
    /// <param name="toAddresses">Email addresses to send the email to.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="body">Email body.</param>
    /// <param name="isHtml">Indicates whether the email body is HTML.</param>
    /// <param name="ccAddresses">Optional email addresses to send a carbon copy to.</param>
    /// <param name="bccAddresses">Optional email addresses to send a blind carbon copy to.</param>
    /// <param name="attachments">Optional file paths for attachments.</param>

    Task SendEmailAsync(
        string[] toAddresses,
        string subject,
        string body,
        bool isHtml = false,
        string[]? ccAddresses = null,
        string[]? bccAddresses = null,
        string[]? attachments = null
    );
}