using CleanArchitectureTemplate.Application.Dtos.Identity;
using CleanArchitectureTemplate.Application.Features.Users.Commands.RegisterUser;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Abstractions.Services;

/// <summary>
/// Represents the user service interface.
/// </summary>
public interface IUserService : IService
{
    /// <summary>
    /// Creates a new application user.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>The user data transfer object.</returns>
    Task<UserDto?> CreateAsync(RegisterUserCommandRequest model);

    /// <summary>
    /// Updates refresh token for the user.
    /// </summary>
    /// <remarks>
    /// The expiration date of the refresh token is calculated based adding some amount of time to the access token creation date.
    /// This time is defined in the appsettings.json file.
    /// </remarks>
    /// <param name="userId">The user id.</param>
    /// <param name="refreshToken">The refresh token.</param>
    /// <param name="accessTokenCreationTime">The access token creation date.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task UpdateRefreshTokenAsync(Guid userId, string refreshToken, DateTime accessTokenCreationTime);
}