using CommercePortal.Domain.Entities.Ordering;

namespace CommercePortal.Application.Abstractions.Repositories.Ordering;

/// <summary>
/// Represents the read repository interface for the <see cref="OrderItem"/> entity.
/// </summary>
public interface IOrderItemReadRepository : IReadRepository<OrderItem>
{
}