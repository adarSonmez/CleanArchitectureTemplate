using CleanArchitectureTemplate.Application.Abstractions.Repositories.Marketing;
using CleanArchitectureTemplate.Domain.Entities.Marketing;
using CleanArchitectureTemplate.Persistence.Contexts;

namespace CleanArchitectureTemplate.Persistence.Repositories.EntityFramework.Marketing;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Category"/> read repository.
/// </summary>
public class EfCategoryReadRepository(EfDbContext context) : EfReadRepository<Category>(context), ICategoryReadRepository
{
}