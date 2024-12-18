namespace CommercePortal.Application.DTOs;

/// <summary>
/// Represents a token used for session management.
/// </summary>
public class Token
{
    /// <summary>
    /// Gets or sets the access token.
    /// </summary>
    public string? AccessToken { get; set; }

    /// <summary>
    /// Gets or sets the expiration time of the token.
    /// </summary>
    public DateTime ExpirationTime { get; set; }

    /// <summary>
    /// Gets or sets the refresh token.
    /// </summary>
    public string? RefreshToken { get; set; }
}