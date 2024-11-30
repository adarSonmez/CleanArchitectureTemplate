using CommercePortal.Application.Repositories;
using CommercePortal.Domain.Entities;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Order"/> write repository.
/// </summary>
public class EfOrderWriteRepository(EfDbContext context) : EfWriteRepository<Order>(context), IOrderWriteRepository
{
}