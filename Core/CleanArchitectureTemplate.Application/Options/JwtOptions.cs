namespace CleanArchitectureTemplate.Application.Options;

/// <summary>
/// Options used for configuring JWT authentication behavior.
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// The secret key used to sign JWT tokens.
    /// </summary>
    public required string SecretKey { get; set; }

    /// <summary>
    /// The issuer of the JWT token.
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// The intended audience of the JWT token.
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// The expiration time in minutes for access tokens.
    /// </summary>
    public int AccessTokenExpiration { get; set; } = 360;

    /// <summary>
    /// The expiration time in minutes for refresh tokens.
    /// </summary>
    public int RefreshTokenExpiration { get; set; } = 60;
}