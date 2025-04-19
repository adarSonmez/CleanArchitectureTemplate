using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CleanArchitectureTemplate.Infrastructure.Services.Mailing.MimeKit;

/// <summary>
/// Represents an email service that uses MimeKit to send emails.
/// </summary>
public class MimeKitEmailService : IEmailService
{
    private readonly ILogger<MimeKitEmailService> _logger;
    private readonly SmtpOptions _smtpOptions;

    public MimeKitEmailService(IOptions<SmtpOptions> smtpOptions, ILogger<MimeKitEmailService> logger)
    {
        _smtpOptions = smtpOptions.Value;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task SendEmailAsync(
        string[] toAddresses,
        string subject,
        string body,
        bool isHtml = false,
        string[]? ccAddresses = null,
        string[]? bccAddresses = null,
        string[]? attachments = null)
    {
        try
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_smtpOptions.FromName, _smtpOptions.FromEmail));

            if (toAddresses != null && toAddresses.Length > 0)
            {
                foreach (var to in toAddresses)
                {
                    email.To.Add(new MailboxAddress(to, to));
                }
            }
            else
            {
                throw new ArgumentException("At least one recipient (To) must be specified.");
            }

            if (ccAddresses != null)
            {
                foreach (var cc in ccAddresses)
                {
                    email.Cc.Add(new MailboxAddress(cc, cc));
                }
            }

            if (bccAddresses != null)
            {
                foreach (var bcc in bccAddresses)
                {
                    email.Bcc.Add(new MailboxAddress(bcc, bcc));
                }
            }

            email.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = isHtml ? body : null,
                TextBody = isHtml ? null : body
            };

            if (attachments != null && attachments.Length != 0)
            {
                foreach (var attachmentPath in attachments)
                {
                    if (File.Exists(attachmentPath))
                    {
                        bodyBuilder.Attachments.Add(attachmentPath);
                    }
                    else
                    {
                        _logger.LogWarning("Attachment file not found: {FilePath}", attachmentPath);
                    }
                }
            }

            email.Body = bodyBuilder.ToMessageBody();

            // Send email
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_smtpOptions.Host,
                                    _smtpOptions.Port,
                                    _smtpOptions.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None);
            await smtp.AuthenticateAsync(_smtpOptions.UserName, _smtpOptions.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            _logger.LogInformation("Email successfully sent to {Recipients}", string.Join(", ", toAddresses));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {Recipients}", string.Join(", ", toAddresses));
            throw;
        }
    }
}