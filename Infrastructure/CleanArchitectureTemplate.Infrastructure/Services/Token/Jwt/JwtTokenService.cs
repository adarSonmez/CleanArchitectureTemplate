using CleanArchitectureTemplate.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
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
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// </inheritdoc>
    public DTO::TokenDto GenerateToken(string userName, IList<string> roles, bool? infiniteExpiration = false)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var expirationDate = infiniteExpiration == true
            ? DateTime.UtcNow.AddYears(500)
            : DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:AccessTokenExpiration"]));

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, userName)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var jwtSecurityToken = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
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

    /// <inheritdoc />
    public ClaimsPrincipal? ValidateToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
            return principal;
        }
        catch
        {
            return null;
        }
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