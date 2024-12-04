using CommercePortal.Domain.Entities;

namespace CommercePortal.Application.Repositories.Products;

/// <summary>
/// Represents the write repository interface for the <see cref="Product"/> entity.
/// </summary>
public interface IProductWriteRepository : IWriteRepository<Product>
{
}