using CommercePortal.Application.Repositories.Files;
using E = CommercePortal.Domain.Entities;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Files;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="E.File"/> write repository.
/// </summary>
public class EfFileWriteRepository(EfDbContext context) : EfWriteRepository<E::File>(context), IFileWriteRepository
{
}