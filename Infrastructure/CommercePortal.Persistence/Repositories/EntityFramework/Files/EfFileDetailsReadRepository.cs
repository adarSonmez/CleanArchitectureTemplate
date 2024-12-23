using CommercePortal.Application.Abstractions.Repositories.Files;
using CommercePortal.Domain.Entities.Files;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Files;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="IFileDetailsReadRepository"/>.
/// </summary>
public class EfFileDetailsReadRepository(EfDbContext context) : EfReadRepository<FileDetails>(context), IFileDetailsReadRepository
{
}