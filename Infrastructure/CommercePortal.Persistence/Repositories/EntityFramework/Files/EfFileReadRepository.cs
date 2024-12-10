using CommercePortal.Application.Repositories.Files;
using CommercePortal.Persistence.Contexts;
using E = CommercePortal.Domain.Entities;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Files;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="E.File"/> read repository.
/// </summary>
public class EfFileReadRepository(EfDbContext context) : EfReadRepository<E::File>(context), IFileReadRepository
{
}