using CommercePortal.Application.Repositories.Files;
using CommercePortal.Domain.Entities;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Files;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="ProductImageFile"/> write repository.
/// </summary>
public class EfProductImageFileWriteRepository(EfDbContext context) : EfWriteRepository<ProductImageFile>(context), IProductImageFileWriteRepository
{
}