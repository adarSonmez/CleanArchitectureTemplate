using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Persistence.Contexts;

namespace CleanArchitectureTemplate.Persistence.Repositories.EntityFramework.Shopping;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Basket"/> write repository.
/// </summary>
public class EfBasketWriteRepository(EfDbContext context) : EfWriteRepository<Basket>(context), IBasketWriteRepository
{
}