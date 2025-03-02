using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Abstractions.Services;

/// <summary>
/// Represents an interface for handling token-related operations.
/// </summary>
public interface ITokenService : IService
{
    /// <summary>
    /// Generates a token for the specified user.
    /// </summary>
    /// <param name="userName">The user name.</param>
    /// <param name="roles">The roles of the user.</param>
    /// <param name="infiniteExpiration">Indicates if the token has infinite expiration.</param>
    /// <returns>The generated token.</returns>
    TokenDto GenerateToken(string userName, IList<string> roles, bool? infiniteExpiration = false);
}