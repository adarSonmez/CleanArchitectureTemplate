using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.DTOs;

/// <summary>
/// Represents the token data transfer object.
/// </summary>
/// <param name="AccessToken">The access token.</param>
/// <param name="ExpirationTime">The expiration time of the token.</param>
/// <param name="RefreshToken">The refresh token.</param>
public record TokenDto
(
    string? AccessToken,
    DateTime ExpirationTime,
    string? RefreshToken
) : IDto;