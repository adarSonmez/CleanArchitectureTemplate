using CommercePortal.Application.Repositories.Products;
using CommercePortal.Domain.Entities;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Products;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Product"/> read repository.
/// </summary>
public class EfProductReadRepository(EfDbContext context) : EfReadRepository<Product>(context), IProductReadRepository
{
}