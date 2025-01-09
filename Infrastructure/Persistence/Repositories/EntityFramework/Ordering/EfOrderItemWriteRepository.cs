using CleanArchitectureTemplate.Application.Abstractions.Repositories.Ordering;
using CleanArchitectureTemplate.Domain.Entities.Ordering;
using CleanArchitectureTemplate.Persistence.Contexts;

namespace CleanArchitectureTemplate.Persistence.Repositories.EntityFramework.Ordering;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="OrderItem"/> write repository.
/// </summary>
public class EfOrderItemWriteRepository(EfDbContext context) : EfWriteRepository<OrderItem>(context), IOrderItemWriteRepository
{
}