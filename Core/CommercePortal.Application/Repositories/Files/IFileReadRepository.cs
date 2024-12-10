using E = CommercePortal.Domain.Entities;

namespace CommercePortal.Application.Repositories.Files;

/// <summary>
/// Represents the read repository interface for the <see cref="E.File"/> entity.
/// </summary>
public interface IFileReadRepository : IReadRepository<E::File>
{
}