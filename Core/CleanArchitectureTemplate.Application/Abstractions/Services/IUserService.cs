using CleanArchitectureTemplate.Application.Dtos.Identity;
using CleanArchitectureTemplate.Application.Features.AppUsers.Commands.RegisterAppUser;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Abstractions.Services;

/// <summary>
/// Represents the user service interface.
/// </summary>
public interface IUserService : IService
{
    /// <summary>
    /// Creates a new application user.
    Task<UserDto?> CreateAsync(RegisterAppUserCommandRequest model);
}