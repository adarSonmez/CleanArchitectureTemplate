using CommercePortal.Application.Abstractions.Repositories.Marketing;
using CommercePortal.Domain.Entities.Marketing;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Marketing;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Product"/> read repository.
/// </summary>
public class EfProductReadRepository(EfDbContext context) : EfReadRepository<Product>(context), IProductReadRepository
{
}