using CommercePortal.Application.Repositories;
using CommercePortal.Domain.Entities;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Product"/> read repository.
/// </summary>
public class EfProductReadRepository(EfDbContext context) : EfReadRepository<Product>(context), IProductReadRepository
{
}