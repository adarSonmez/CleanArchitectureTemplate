using DTO = CommercePortal.Application.DTOs;

namespace CommercePortal.Application.Abstractions.Services.Token;

/// <summary>
/// Represents an interface for handling tokens.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generates a token for the specified user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="infiniteExpiration">Indicates if the token has infinite expiration.</param>
    /// <returns>The generated token.</returns>
    DTO::TokenDTO GenerateToken(Guid userId, bool? infiniteExpiration = false);
}