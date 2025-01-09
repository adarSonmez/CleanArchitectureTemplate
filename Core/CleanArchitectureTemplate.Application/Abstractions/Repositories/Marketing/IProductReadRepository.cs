using CleanArchitectureTemplate.Domain.Entities.Marketing;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories.Marketing;

/// <summary>
/// Represents the read repository interface for the <see cref="Category"/> entity.
/// </summary>
public interface ICategoryReadRepository : IReadRepository<Category>
{
}