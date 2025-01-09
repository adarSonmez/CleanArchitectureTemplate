using CleanArchitectureTemplate.Application.Abstractions.Repositories.Files;
using CleanArchitectureTemplate.Domain.Entities.Files;
using CleanArchitectureTemplate.Persistence.Contexts;

namespace CleanArchitectureTemplate.Persistence.Repositories.EntityFramework.Files;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="IProductImageFileReadRepository"/>.
/// </summary>
public class EfProductImageFileReadRepository(EfDbContext context) : EfReadRepository<ProductImageFile>(context), IProductImageFileReadRepository
{
}