using CommercePortal.Application.Abstractions.Repositories.Membership;
using CommercePortal.Domain.Entities.Membership;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Membership;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Store"/> write repository.
/// </summary>
public class EfStoreWriteRepository(EfDbContext context) : EfWriteRepository<Store>(context), IStoreWriteRepository
{
}