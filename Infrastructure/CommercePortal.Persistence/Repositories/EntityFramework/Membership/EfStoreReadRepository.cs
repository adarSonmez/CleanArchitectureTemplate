using CommercePortal.Application.Abstractions.Repositories.Membership;
using CommercePortal.Domain.Entities.Membership;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Membership;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Store"/> read repository.
/// </summary>
public class EfStoreReadRepository(EfDbContext context) : EfReadRepository<Store>(context), IStoreReadRepository
{
}