using CommercePortal.Domain.Entities.Files;

namespace CommercePortal.Application.Abstractions.Repositories.Files;

/// <summary>
/// Represents the read repository interface for the <see cref="UserAvatarFile"/> entity.
/// </summary>
public interface IUserAvatarFileReadRepository : IReadRepository<UserAvatarFile>
{
}