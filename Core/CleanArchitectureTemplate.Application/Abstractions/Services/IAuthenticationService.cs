using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.Features.AppUsers.Commands.LoginAppUser;
using CleanArchitectureTemplate.Application.Features.Commands.AppUsers.FacebookLoginAppUser;
using CleanArchitectureTemplate.Application.Features.Commands.AppUsers.GoogleLoginAppUser;
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
    Task<TokenDTO?> InternalLoginAsync(LoginAppUserCommandRequest model);

    #endregion Internal Authentication

    #region External Authentication

    /// <summary>
    /// Authenticates the user using Facebook.
    /// </summary>
    /// <param name="model">Model containing the Facebook user credentials.</param>
    /// <returns>The generated token if the user is authenticated; otherwise, null.</returns>
    Task<TokenDTO?> FacebookLoginAsync(FacebookLoginAppUserCommandRequest model);

    /// <summary>
    /// Authenticates the user using Google.
    /// </summary>
    /// <param name="model">Model containing the Google user credentials.</param>
    /// <returns>The generated token if the user is authenticated; otherwise, null.</returns>
    Task<TokenDTO?> GoogleLoginAsync(GoogleLoginAppUserCommandRequest model);

    #endregion External Authentication
}