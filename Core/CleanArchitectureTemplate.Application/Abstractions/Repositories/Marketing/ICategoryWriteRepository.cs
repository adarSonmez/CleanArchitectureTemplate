using CleanArchitectureTemplate.Domain.Entities.Marketing;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories.Marketing;

/// <summary>
/// Represents the write repository interface for the <see cref="Category"/> entity.
/// </summary>
public interface ICategoryWriteRepository : IWriteRepository<Category>
{
}