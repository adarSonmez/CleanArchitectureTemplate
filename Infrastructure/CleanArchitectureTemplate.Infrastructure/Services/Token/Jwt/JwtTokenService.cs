using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DTO = CleanArchitectureTemplate.Application.DTOs;

namespace CleanArchitectureTemplate.Infrastructure.Services.Token.Jwt;

/// <summary>
/// Represents a handler for generating and validating JWT tokens.
/// </summary>
public class JwtTokenService : ITokenService
{
    private readonly JwtOptions _jwtOptions;

    public JwtTokenService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    /// </inheritdoc>
    public DTO::TokenDto GenerateToken(string userName, IList<string> roles, bool? infiniteExpiration = false)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey!));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var expirationDate = infiniteExpiration == true
            ? DateTime.UtcNow.AddYears(500)
            : DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtOptions.AccessTokenExpiration));

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, userName)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            expires: expirationDate,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials,
            claims: claims
        );

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var accessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
        var refreshToken = GenerateRefreshToken();

        return new DTO::TokenDto(accessToken, expirationDate, refreshToken);
    }

    /// <summary>
    /// Generates a refresh token to be used for refreshing the access token.
    /// </summary>
    /// <returns>The generated refresh token.</returns>
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}