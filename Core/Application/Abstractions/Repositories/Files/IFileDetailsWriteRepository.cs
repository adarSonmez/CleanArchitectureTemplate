using CleanArchitectureTemplate.Domain.Entities.Files;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories.Files;

/// <summary>
/// Represents the Write repository interface for the <see cref="FileDetails"/> entity.
/// </summary>
public interface IFileDetailsWriteRepository : IWriteRepository<FileDetails>
{
}