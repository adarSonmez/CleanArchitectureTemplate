using CommercePortal.Application.Abstractions.Repositories.Ordering;
using CommercePortal.Domain.Entities.Ordering;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Ordering;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Order"/> write repository.
/// </summary>
public class EfOrderWriteRepository(EfDbContext context) : EfWriteRepository<Order>(context), IOrderWriteRepository
{
}