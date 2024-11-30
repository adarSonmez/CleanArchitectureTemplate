using CommercePortal.Application.Repositories;
using CommercePortal.Domain.Entities;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework
{
    /// <summary>
    /// Represents EntityFramework implementation of the <see cref="Order"/> read repository.
    /// </summary>
    public class EfOrderReadRepository(EfDbContext context) : EfReadRepository<Order>(context), IOrderReadRepository
    {
    }
}