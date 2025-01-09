using CleanArchitectureTemplate.Domain.Entities.Membership;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories.Membership;

/// <summary>
/// Represents the read repository interface for the <see cref="Store"/> entity.
/// </summary>
public interface IStoreReadRepository : IReadRepository<Store>
{
}