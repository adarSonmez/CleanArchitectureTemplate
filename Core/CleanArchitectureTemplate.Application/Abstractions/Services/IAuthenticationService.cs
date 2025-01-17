using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.Features.Auth.Commands.FacebookLogin;
using CleanArchitectureTemplate.Application.Features.Auth.Commands.GoogleLogin;
using CleanArchitectureTemplate.Application.Features.Auth.Commands.InternalLogin;
using CleanArchitectureTemplate.Application.Features.Auth.Commands.RefreshToken;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Abstractions.Services;

/// <summary>
/// Represents the authentication service interface for internal and external authentications.
/// </summary>
public interface IAuthenticationService : IService
{
    #region Internal Authentication

    /// <summary>
    /// Authenticates the specified user based on the provided credentials.
    /// </summary>
    /// <param name="model">Model containing the user credentials.</param>
    /// <returns>The generated token if the user is authenticated; otherwise, null.</returns>
    Task<TokenDto?> InternalLoginAsync(InternalLoginCommandRequest model);

    #endregion Internal Authentication

    #region External Authentication

    /// <summary>
    /// Authenticates the user using Facebook.
    /// </summary>
    /// <param name="model">Model containing the Facebook user credentials.</param>
    /// <returns>The generated token if the user is authenticated; otherwise, null.</returns>
    Task<TokenDto?> FacebookLoginAsync(FacebookLoginCommandRequest model);

    /// <summary>
    /// Authenticates the user using Google.
    /// </summary>
    /// <param name="model">Model containing the Google user credentials.</param>
    /// <returns>The generated token if the user is authenticated; otherwise, null.</returns>
    Task<TokenDto?> GoogleLoginAsync(GoogleLoginCommandRequest model);

    #endregion External Authentication

    #region Refresh Token

    /// <summary>
    /// Refreshes the user token and enables the user to continue using the application.
    /// </summary>
    /// <param name="model">Model containing the refresh token and the user ID.</param>
    Task<TokenDto?> RefreshTokenAsync(RefreshTokenCommandRequest model);

    #endregion Refresh Token
}