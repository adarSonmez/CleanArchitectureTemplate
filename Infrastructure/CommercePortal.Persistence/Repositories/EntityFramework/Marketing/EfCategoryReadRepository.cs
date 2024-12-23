using CommercePortal.Application.Abstractions.Repositories.Marketing;
using CommercePortal.Domain.Entities.Marketing;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Marketing;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Category"/> read repository.
/// </summary>
public class EfCategoryReadRepository(EfDbContext context) : EfReadRepository<Category>(context), ICategoryReadRepository
{
}