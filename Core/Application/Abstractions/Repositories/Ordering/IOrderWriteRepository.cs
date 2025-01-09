using CleanArchitectureTemplate.Domain.Entities.Ordering;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories.Ordering;

/// <summary>
/// Represents the write repository interface for the <see cref="Order"/> entity.
/// </summary>
public interface IOrderWriteRepository : IWriteRepository<Order>
{
}