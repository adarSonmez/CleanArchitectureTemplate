using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Persistence.Contexts;

namespace CleanArchitectureTemplate.Persistence.Repositories.EntityFramework.Shopping;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Basket"/> read repository.
/// </summary>
public class EfBasketReadRepository(EfDbContext context) : EfReadRepository<Basket>(context), IBasketReadRepository
{
}