using CleanArchitectureTemplate.Domain.Entities.Shopping;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;

/// <summary>
/// Represents the read repository interface for the <see cref="Category"/> entity.
/// </summary>
public interface ICategoryReadRepository : IReadRepository<Category>
{
}