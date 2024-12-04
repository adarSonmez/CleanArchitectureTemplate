using CommercePortal.Application.Repositories.Products;
using CommercePortal.Domain.Entities;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Products;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Product"/> write repository.
/// </summary>
public class EfProductWriteRepository(EfDbContext context) : EfWriteRepository<Product>(context), IProductWriteRepository
{
}