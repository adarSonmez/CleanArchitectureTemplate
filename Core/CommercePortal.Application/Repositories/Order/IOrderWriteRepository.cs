using CommercePortal.Domain.Entities;

namespace CommercePortal.Application.Repositories;

/// <summary>
/// Represents the write repository interface for the <see cref="Order"/> entity.
/// </summary>
public interface IOrderWriteRepository : IWriteRepository<Order>
{
}