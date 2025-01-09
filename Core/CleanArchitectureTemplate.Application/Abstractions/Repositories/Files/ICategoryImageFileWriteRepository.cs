using CleanArchitectureTemplate.Domain.Entities.Files;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories.Files;

/// <summary>
/// Represents the read repository interface for the <see cref="CategoryImageFile"/> entity.
/// </summary>
public interface ICategoryImageFileWriteRepository : IWriteRepository<CategoryImageFile>
{
}