using CommercePortal.Application.Repositories.Files;
using CommercePortal.Persistence.Contexts;
using E = CommercePortal.Domain.Entities;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Files;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="E.File"/> write repository.
/// </summary>
public class EfFileWriteRepository(EfDbContext context) : EfWriteRepository<E::File>(context), IFileWriteRepository
{
}