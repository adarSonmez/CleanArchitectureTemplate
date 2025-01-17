using AutoMapper;
using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Dtos.Identity;
using CleanArchitectureTemplate.Application.Features.Users.Commands.RegisterUser;
using CleanArchitectureTemplate.Domain.Common;
using CleanArchitectureTemplate.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace CleanArchitectureTemplate.Persistence.Services.User.Identity;

/// <summary>
/// Represents an user service that is on top of the ASP.NET Core Identity mechanism.
/// </summary>
public class IdentityUserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;

    public IdentityUserService(IConfiguration configuration, IMapper mapper, UserManager<AppUser> userManager)
    {
        _configuration = configuration;
        _mapper = mapper;
        _userManager = userManager;
    }

    /// <inheritdoc />
    public async Task<UserDto?> CreateAsync(RegisterUserCommandRequest model)
    {
        var user = _mapper.Map<AppUser>(model);
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            return _mapper.Map<UserDto>(user);
        }
        if (result.Errors.Any())
        {
            var errors = result.Errors.Select(e => e.Description);
            throw new Exception($"Failed to create user: {string.Join(", ", errors)}");
        }
        else
        {
            throw new Exception("Failed to create user");
        }
    }

    /// <inheritdoc />
    public async Task UpdateRefreshTokenAsync(Guid userId, string refreshToken, DateTime accessTokenCreationTime)
    {
        var refreshTokenExpirationAddition = Convert.ToDouble(_configuration["Jwt:RefreshTokenExpiration"]);
        var refreshTokenExpirationTime = accessTokenCreationTime.AddMinutes(refreshTokenExpirationAddition);
        var user = await _userManager.FindByIdAsync(userId.ToString());

        BusinessRules.Run(("USR211795", BusinessRules.CheckEntityNull(user)));

        user!.RefreshToken = refreshToken;
        user.RefreshTokenExpiration = refreshTokenExpirationTime;

        await _userManager.UpdateAsync(user);
    }
}