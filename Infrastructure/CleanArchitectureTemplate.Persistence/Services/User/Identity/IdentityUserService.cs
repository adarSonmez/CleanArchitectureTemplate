using AutoMapper;
using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Dtos.Identity;
using CleanArchitectureTemplate.Application.Features.AppUsers.Commands.RegisterAppUser;
using CleanArchitectureTemplate.Persistence.Identity;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitectureTemplate.Persistence.Services.User.Identity;

/// <summary>
/// Represents an user service that is on top of the ASP.NET Core Identity mechanism.
/// </summary>
public class IdentityUserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;

    public IdentityUserService(IMapper mapper, UserManager<AppUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    /// <inheritdoc />
    public async Task<UserDto?> CreateAsync(RegisterAppUserCommandRequest model)
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
}