using CommercePortal.Domain.Entities;

namespace CommercePortal.Application.Repositories.Orders;

/// <summary>
/// Represents the read repository interface for the <see cref="Order"/> entity.
/// </summary>
public interface IOrderReadRepository : IReadRepository<Order>
{
}