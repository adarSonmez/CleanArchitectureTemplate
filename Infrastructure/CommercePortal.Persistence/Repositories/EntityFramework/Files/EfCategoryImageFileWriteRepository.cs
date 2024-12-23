using CommercePortal.Application.Abstractions.Repositories.Files;
using CommercePortal.Domain.Entities.Files;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Files;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="ICategoryImageFileWriteRepository"/>.
/// </summary>
public class EfCategoryImageFileWriteRepository(EfDbContext context) : EfWriteRepository<CategoryImageFile>(context), ICategoryImageFileWriteRepository
{
}