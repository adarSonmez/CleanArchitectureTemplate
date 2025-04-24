using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Domain.Entities.Identity;
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
    /// <param name="user">The user for whom the token is generated.</param>
    /// <param name="roles">The roles of the user.</param>
    /// <param name="infiniteExpiration">Indicates if the token has infinite expiration.</param>
    /// <returns>The generated token.</returns>
    TokenDto GenerateToken(DomainUser user, IList<string> roles, bool? infiniteExpiration = false);
}