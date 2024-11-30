using CommercePortal.Application.Repositories;
using CommercePortal.Domain.Entities;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Product"/> write repository.
/// </summary>
public class EfProductWriteRepository(EfDbContext context) : EfWriteRepository<Product>(context), IProductWriteRepository
{
}