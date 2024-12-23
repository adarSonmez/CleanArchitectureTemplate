using CommercePortal.Domain.Entities.Files;

namespace CommercePortal.Application.Abstractions.Repositories.Files;

/// <summary>
/// Represents the Write repository interface for the <see cref="FileDetails"/> entity.
/// </summary>
public interface IFileDetailsWriteRepository : IWriteRepository<FileDetails>
{
}