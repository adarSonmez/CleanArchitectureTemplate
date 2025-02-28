using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace CleanArchitectureTemplate.Infrastructure.Services.Cookie;

/// <summary>
/// Represents a service for managing cookies.
/// </summary>
public class CookieService : ICookieService
{
    private const string AccessTokenCookieName = "AccessToken";
    private const string RefreshTokenCookieName = "RefreshToken";
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly CookieOptions _authCookieOptions;

    public CookieService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;

        _authCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        };
    }

    /// <inheritdoc/>
    public void SetAuthCookies(TokenDto tokenDto)
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Append(AccessTokenCookieName, tokenDto.AccessToken!, _authCookieOptions);
        _httpContextAccessor.HttpContext?.Response.Cookies.Append(RefreshTokenCookieName, tokenDto.RefreshToken!, _authCookieOptions);
    }

    /// <inheritdoc/>
    public void ClearAuthCookies()
    {
        ClearAccessTokenCookie();
        ClearRefreshTokenCookie();
    }

    /// <inheritdoc/>
    public void ClearRefreshTokenCookie()
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Append(RefreshTokenCookieName, string.Empty, _authCookieOptions);
    }

    /// <inheritdoc/>
    public void ClearAccessTokenCookie()
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Append(AccessTokenCookieName, string.Empty, _authCookieOptions);
    }

    /// <inheritdoc/>
    public string? GetAccessTokenFromCookie()
    {
        return _httpContextAccessor.HttpContext!.Request.Cookies.TryGetValue(AccessTokenCookieName, out var token) ? token : null;
    }

    /// <inheritdoc/>
    public string? GetRefreshTokenFromCookie()
    {
        return _httpContextAccessor.HttpContext!.Request.Cookies.TryGetValue(AccessTokenCookieName, out var token) ? token : null;
    }
}