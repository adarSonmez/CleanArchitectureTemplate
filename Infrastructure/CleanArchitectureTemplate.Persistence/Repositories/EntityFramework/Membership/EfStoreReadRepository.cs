using CleanArchitectureTemplate.Application.Abstractions.Repositories.Membership;
using CleanArchitectureTemplate.Domain.Entities.Membership;
using CleanArchitectureTemplate.Persistence.Contexts;

namespace CleanArchitectureTemplate.Persistence.Repositories.EntityFramework.Membership;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Store"/> read repository.
/// </summary>
public class EfStoreReadRepository(EfDbContext context) : EfReadRepository<Store>(context), IStoreReadRepository
{
}