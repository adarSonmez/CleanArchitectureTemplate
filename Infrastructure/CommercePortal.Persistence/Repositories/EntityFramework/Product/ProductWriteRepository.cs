using CommercePortal.Application.Repositories;
using CommercePortal.Domain.Entities;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework
{
    /// <summary>
    /// Represents the <see cref="Product"/> write repository.
    /// </summary>
    public class ProductWriteRepository : EfWriteRepository<Product, EfDbContext>, IProductWriteRepository
    {
    }
}