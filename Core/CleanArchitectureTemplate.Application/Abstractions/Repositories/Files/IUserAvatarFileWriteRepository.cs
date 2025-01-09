using CleanArchitectureTemplate.Domain.Entities.Files;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories.Files;

/// <summary>
/// Represents the read repository interface for the <see cref="UserAvatarFile"/> entity.
/// </summary>
public interface IUserAvatarFileWriteRepository : IWriteRepository<UserAvatarFile>
{
}