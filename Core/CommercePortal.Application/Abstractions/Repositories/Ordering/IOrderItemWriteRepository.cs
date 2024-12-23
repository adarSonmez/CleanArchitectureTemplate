using CommercePortal.Domain.Entities.Ordering;

namespace CommercePortal.Application.Abstractions.Repositories.Ordering;

/// <summary>
/// Represents the write repository interface for the <see cref="OrderItem"/> entity.
/// </summary>
public interface IOrderItemWriteRepository : IWriteRepository<OrderItem>
{
}