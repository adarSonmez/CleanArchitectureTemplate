using CleanArchitectureTemplate.Application.Abstractions.Repositories.Marketing;
using CleanArchitectureTemplate.Domain.Entities.Marketing;
using CleanArchitectureTemplate.Persistence.Contexts;

namespace CleanArchitectureTemplate.Persistence.Repositories.EntityFramework.Marketing;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Product"/> write repository.
/// </summary>
public class EfProductWriteRepository(EfDbContext context) : EfWriteRepository<Product>(context), IProductWriteRepository
{
}