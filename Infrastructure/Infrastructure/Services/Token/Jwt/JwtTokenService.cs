﻿using CleanArchitectureTemplate.Application.Abstractions.Services.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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
    public DTO::TokenDTO GenerateToken(Guid userId, bool? infiniteExpiration = false)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var expirationDate = infiniteExpiration == true
            ? DateTime.MaxValue
            : DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:AccessExpiration"]));

        var jwtSecurityToken = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            expires: expirationDate,
            notBefore: DateTime.Now,
            signingCredentials: signingCredentials
        // claims: SetClaims(userId.ToString(), username, email, role)
        );

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var accessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
        // token.RefreshToken = CreateRefreshToken();

        return new DTO::TokenDTO(accessToken, expirationDate, "");
    }
}