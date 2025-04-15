namespace CleanArchitectureTemplate.Application.Options;

/// <summary>
/// Options used to configure SMTP email sending.
/// </summary>
public class SmtpOptions
{
    /// <summary>
    /// The SMTP server host name.
    /// </summary>
    public required string Host { get; set; }

    /// <summary>
    /// The SMTP server port.
    /// </summary>
    public int Port { get; set; } = 587;

    /// <summary>
    /// Whether SSL should be enabled for SMTP.
    /// </summary>
    public bool EnableSsl { get; set; } = true;

    /// <summary>
    /// The username for authenticating with the SMTP server.
    /// </summary>
    public required string UserName { get; set; }

    /// <summary>
    /// The password for authenticating with the SMTP server.
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// The email address used as the sender in outgoing emails.
    /// </summary>
    public required string FromEmail { get; set; }

    /// <summary>
    /// The display name used in outgoing emails.
    /// </summary>
    public string FromName { get; set; } = string.Empty;
}