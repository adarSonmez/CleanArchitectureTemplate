using CleanArchitectureTemplate.Domain.Entities.Shopping;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;

/// <summary>
/// Represents the read repository interface for the <see cref="Product"/> entity.
/// </summary>
public interface IProductReadRepository : IReadRepository<Product>
{
}