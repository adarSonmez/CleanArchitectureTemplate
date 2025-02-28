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
}