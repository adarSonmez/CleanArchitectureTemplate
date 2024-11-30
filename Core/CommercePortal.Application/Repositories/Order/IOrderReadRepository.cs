using CommercePortal.Domain.Entities;

namespace CommercePortal.Application.Repositories;

/// <summary>
/// Represents the read repository interface for the <see cref="Order"/> entity.
/// </summary>
public interface IOrderReadRepository : IReadRepository<Order>
{
}