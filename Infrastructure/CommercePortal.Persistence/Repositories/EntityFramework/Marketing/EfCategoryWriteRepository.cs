using CommercePortal.Application.Abstractions.Repositories.Marketing;
using CommercePortal.Domain.Entities.Marketing;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Marketing;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Category"/> write repository.
/// </summary>
public class EfCategoryWriteRepository(EfDbContext context) : EfWriteRepository<Category>(context), ICategoryWriteRepository
{
}