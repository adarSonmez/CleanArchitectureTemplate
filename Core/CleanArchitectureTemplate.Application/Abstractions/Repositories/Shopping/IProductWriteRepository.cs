using CleanArchitectureTemplate.Domain.Entities.Shopping;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;

/// <summary>
/// Represents the write repository interface for the <see cref="Product"/> entity.
/// </summary>
public interface IProductWriteRepository : IWriteRepository<Product>
{
}