using CommercePortal.Application.Abstractions.Repositories.Ordering;
using CommercePortal.Domain.Entities.Ordering;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Ordering;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="OrderItem"/> read repository.
/// </summary>
public class EfOrderItemReadRepository(EfDbContext context) : EfReadRepository<OrderItem>(context), IOrderItemReadRepository
{
}