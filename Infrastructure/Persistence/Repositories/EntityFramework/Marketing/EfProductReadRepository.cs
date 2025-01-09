using CleanArchitectureTemplate.Application.Abstractions.Repositories.Marketing;
using CleanArchitectureTemplate.Domain.Entities.Marketing;
using CleanArchitectureTemplate.Persistence.Contexts;

namespace CleanArchitectureTemplate.Persistence.Repositories.EntityFramework.Marketing;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Product"/> read repository.
/// </summary>
public class EfProductReadRepository(EfDbContext context) : EfReadRepository<Product>(context), IProductReadRepository
{
}