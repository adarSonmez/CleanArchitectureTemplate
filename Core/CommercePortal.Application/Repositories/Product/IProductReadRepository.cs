using CommercePortal.Domain.Entities;

namespace CommercePortal.Application.Repositories;

/// <summary>
/// Represents the read repository interface for the <see cref="Product"/> entity.
/// </summary>
public interface IProductReadRepository : IReadRepository<Product>
{
}