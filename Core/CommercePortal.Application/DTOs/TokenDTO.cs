namespace CommercePortal.Application.DTOs;

/// <summary>
/// Represents the token data transfer object.
/// </summary>
public class TokenDTO
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