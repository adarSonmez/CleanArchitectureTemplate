using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Persistence.Contexts;

namespace CleanArchitectureTemplate.Persistence.Repositories.EntityFramework.Shopping;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Product"/> write repository.
/// </summary>
public class EfProductWriteRepository(EfDbContext context) : EfWriteRepository<Product>(context), IProductWriteRepository
{
}