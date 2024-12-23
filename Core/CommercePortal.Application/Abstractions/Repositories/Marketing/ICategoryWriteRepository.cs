using CommercePortal.Domain.Entities.Marketing;

namespace CommercePortal.Application.Abstractions.Repositories.Marketing;

/// <summary>
/// Represents the write repository interface for the <see cref="Category"/> entity.
/// </summary>
public interface ICategoryWriteRepository : IWriteRepository<Category>
{
}