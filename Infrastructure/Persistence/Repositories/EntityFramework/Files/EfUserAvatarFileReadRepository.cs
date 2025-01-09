using CleanArchitectureTemplate.Application.Abstractions.Repositories.Files;
using CleanArchitectureTemplate.Domain.Entities.Files;
using CleanArchitectureTemplate.Persistence.Contexts;

namespace CleanArchitectureTemplate.Persistence.Repositories.EntityFramework.Files;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="IUserAvatarFileReadRepository"/>.
/// </summary>
public class EfUserAvatarFileReadRepository(EfDbContext context) : EfReadRepository<UserAvatarFile>(context), IUserAvatarFileReadRepository
{
}