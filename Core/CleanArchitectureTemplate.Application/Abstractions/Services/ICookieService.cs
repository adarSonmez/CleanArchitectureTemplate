using CleanArchitectureTemplate.Application.DTOs;

namespace CleanArchitectureTemplate.Application.Abstractions.Services;

/// <summary>
/// Represents a service for managing JWT tokens within HTTP cookies.
/// </summary>
public interface ICookieService
{
    /// <summary>
    /// Stores the access and refresh tokens in HTTP-only cookies.
    /// </summary>
    /// <param name="tokenDto">The token data transfer object containing the access token and expiration time.</param>
    void SetAuthCookies(TokenDto tokenDto);

    /// <summary>
    /// Clears the HTTP-only cookies containing the access and refresh tokens.
    /// </summary>
    void ClearAuthCookies();

    /// <summary>
    /// Clears the refresh token cookie.
    /// </summary>
    void ClearRefreshTokenCookie();

    /// <summary>
    /// Clears the access token cookie.
    /// </summary>
    void ClearAccessTokenCookie();

    /// <summary>
    /// Retrieves the access token from the HTTP-only cookie.
    /// </summary>
    string? GetAccessTokenFromCookie();

    /// <summary>
    /// Retrieves the refresh token from the HTTP-only cookie.
    /// </summary>
    string? GetRefreshTokenFromCookie();
}