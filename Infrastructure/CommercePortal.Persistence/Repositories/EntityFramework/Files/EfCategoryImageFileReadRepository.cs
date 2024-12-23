using CommercePortal.Application.Abstractions.Repositories.Files;
using CommercePortal.Domain.Entities.Files;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Files;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="ICategoryImageFileReadRepository"/>.
/// </summary>
public class EfCategoryImageFileReadRepository(EfDbContext context) : EfReadRepository<CategoryImageFile>(context), ICategoryImageFileReadRepository
{
}