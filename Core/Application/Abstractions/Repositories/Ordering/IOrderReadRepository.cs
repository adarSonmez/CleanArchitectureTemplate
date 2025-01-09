using CleanArchitectureTemplate.Domain.Entities.Ordering;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories.Ordering;

/// <summary>
/// Represents the read repository interface for the <see cref="Order"/> entity.
/// </summary>
public interface IOrderReadRepository : IReadRepository<Order>
{
}