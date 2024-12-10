using CommercePortal.Application.Repositories.Files;
using E = CommercePortal.Domain.Entities;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Files;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="E.File"/> read repository.
/// </summary>
public class EfFileReadRepository(EfDbContext context) : EfReadRepository<E::File>(context), IFileReadRepository
{
}