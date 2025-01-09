using CleanArchitectureTemplate.Application.Abstractions.Repositories.Ordering;
using CleanArchitectureTemplate.Domain.Entities.Ordering;
using CleanArchitectureTemplate.Persistence.Contexts;

namespace CleanArchitectureTemplate.Persistence.Repositories.EntityFramework.Ordering;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Order"/> write repository.
/// </summary>
public class EfOrderWriteRepository(EfDbContext context) : EfWriteRepository<Order>(context), IOrderWriteRepository
{
}