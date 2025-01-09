using CleanArchitectureTemplate.Application.Abstractions.Repositories.Ordering;
using CleanArchitectureTemplate.Domain.Entities.Ordering;
using CleanArchitectureTemplate.Persistence.Contexts;

namespace CleanArchitectureTemplate.Persistence.Repositories.EntityFramework.Ordering;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="OrderItem"/> read repository.
/// </summary>
public class EfOrderItemReadRepository(EfDbContext context) : EfReadRepository<OrderItem>(context), IOrderItemReadRepository
{
}