using CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Persistence.Contexts;

namespace CleanArchitectureTemplate.Persistence.Repositories.EntityFramework.Shopping;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Category"/> read repository.
/// </summary>
public class EfCategoryReadRepository(EfDbContext context) : EfReadRepository<Category>(context), ICategoryReadRepository
{
}