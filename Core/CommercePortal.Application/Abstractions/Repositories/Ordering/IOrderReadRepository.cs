using CommercePortal.Domain.Entities.Ordering;

namespace CommercePortal.Application.Abstractions.Repositories.Ordering;

/// <summary>
/// Represents the read repository interface for the <see cref="Order"/> entity.
/// </summary>
public interface IOrderReadRepository : IReadRepository<Order>
{
}