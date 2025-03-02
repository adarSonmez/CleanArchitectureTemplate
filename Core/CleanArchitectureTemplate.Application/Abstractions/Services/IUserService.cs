using CleanArchitectureTemplate.Application.Dtos.Identity;
using CleanArchitectureTemplate.Application.Features.Users.Commands.RegisterUser;
using CleanArchitectureTemplate.Application.RequestParameters;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Abstractions.Services;

/// <summary>
/// Handles user account management, including creation and password updates.
/// </summary>
public interface IUserService : IService
{
    #region User Management

    /// <summary>
    /// Creates a new application user.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user data transfer object.</returns>
    Task<UserDto?> CreateAsync(RegisterUserCommandRequest model);

    #endregion User Management

    #region User Retrieval

    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the user data transfer object.</returns>
    Task<UserDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Retrieves all users in the system with pagination.
    /// </summary>
    Task<IEnumerable<UserDto>> GetAllPaginatedAsync(Pagination? pagination = null);

    #endregion User Retrieval

    #region Password Management

    /// <summary>
    /// Changes the password for a given user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="currentPassword">The user's current password.</param>
    /// <param name="newPassword">The new password.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of errors, if any.</returns>
    Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);

    /// <summary>
    /// Initiates a password reset process for a user who forgot their password.
    /// </summary>
    /// <param name="email">The email of the user requesting a password reset.</param>
    Task ForgotPasswordAsync(string email);

    /// <summary>
    /// Resets the password for a user using a reset token.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="token">The reset token.</param>
    /// <param name="newPassword">The new password.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of errors, if any.</returns>
    Task ResetPasswordAsync(Guid userId, string token, string newPassword);

    #endregion Password Management
}