using E = CommercePortal.Domain.Entities;

namespace CommercePortal.Application.Repositories.Files;

/// <summary>
/// Represents the Write repository interface for the <see cref="E.File"/> entity.
/// </summary>
public interface IFileWriteRepository : IWriteRepository<E::File>
{
}