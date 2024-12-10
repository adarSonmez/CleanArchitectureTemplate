using CommercePortal.Application.Repositories.Files;
using CommercePortal.Domain.Entities;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Files;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="ProductImageFile"/> read repository.
/// </summary>
public class EfProductImageFileReadRepository(EfDbContext context) : EfReadRepository<ProductImageFile>(context), IProductImageFileReadRepository
{
}