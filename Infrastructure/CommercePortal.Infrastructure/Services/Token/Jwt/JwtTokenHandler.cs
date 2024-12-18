using DTO = CommercePortal.Application.DTOs;
using CommercePortal.Application.Abstractions.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CommercePortal.Infrastructure.Services.Token.Jwt;

/// <summary>
/// Represents a handler for generating and validating JWT tokens.
/// </summary>
public class JwtTokenHandler : ITokenHandler
{
    private readonly IConfiguration _configuration;

    public JwtTokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// </inheritdoc>
    public DTO::Token GenerateToken(Guid userId, bool? infiniteExpiration = false)
    {
        DTO::Token? token = new();
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        token.ExpirationTime = infiniteExpiration == true
            ? DateTime.MaxValue
            : DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:AccessExpiration"]));

        var jwtSecurityToken = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            expires: token.ExpirationTime,
            notBefore: DateTime.Now,
            signingCredentials: signingCredentials
        // claims: SetClaims(userId.ToString(), username, email, role)
        );

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        token.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
        // token.RefreshToken = CreateRefreshToken();

        return token;
    }
}