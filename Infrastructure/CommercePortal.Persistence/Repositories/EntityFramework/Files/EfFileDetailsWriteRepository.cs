using CommercePortal.Application.Abstractions.Repositories.Files;
using CommercePortal.Domain.Entities.Files;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Files;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="IFileDetailsWriteRepository"/>.
/// </summary>
public class EfFileDetailsWriteRepository(EfDbContext context) : EfWriteRepository<FileDetails>(context), IFileDetailsWriteRepository
{
}