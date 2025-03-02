namespace CleanArchitectureTemplate.Application.Abstractions.Services;

/// <summary>
/// Provides information about the currently authenticated user.
/// </summary>
public interface IUserContextService
{
    /// <summary>
    /// Gets the current user's username.
    /// </summary>
    string? GetUserName();

    /// <summary>
    /// Gets the current user's unique identifier.
    /// </summary>
    Guid? GetUserId();

    /// <summary>
    /// Checks if the current user is an Admin. If not, verifies whether the provided userId matches the authenticated user's ID.
    /// </summary>
    /// <param name="userId">The user ID to check.</param>
    /// <returns>True if the user is Admin or the same user; otherwise, false.</returns>
    bool IsAdminOrSelf(Guid userId);

    /// <summary>
    /// Checks if the current user has a specific role.
    /// </summary>
    /// <param name="role">The role to check.</param>
    /// <returns>True if the user has the role; otherwise, false.</returns>
    bool IsInRole(string role);

    /// <summary>
    /// Checks if the current authenticated user matches the provided user ID.
    /// </summary>
    /// <param name="userId">The user ID to check.</param>
    /// <returns>True if the user ID matches the authenticated user; otherwise, false.</returns>
    bool IsCurrentUser(Guid userId);

    /// <summary>
    /// Retrieves the roles of the authenticated user.
    /// </summary>
    /// <returns>A list of roles assigned to the user.</returns>
    List<string> GetUserRoles();

    /// <summary>
    /// Checks if the current user has any of the specified roles.
    /// </summary>
    /// <param name="roles">A list of roles to check.</param>
    /// <returns>True if the user has at least one of the specified roles; otherwise, false.</returns>
    bool HasAnyRole(params string[] roles);

    /// <summary>
    /// Determines whether the user is authenticated.
    /// </summary>
    /// <returns>True if the user is authenticated; otherwise, false.</returns>
    bool IsAuthenticated();
}