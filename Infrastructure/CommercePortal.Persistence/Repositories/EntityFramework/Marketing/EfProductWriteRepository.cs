using CommercePortal.Application.Abstractions.Repositories.Marketing;
using CommercePortal.Domain.Entities.Marketing;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Marketing;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Product"/> write repository.
/// </summary>
public class EfProductWriteRepository(EfDbContext context) : EfWriteRepository<Product>(context), IProductWriteRepository
{
}