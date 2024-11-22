using CommercePortal.Application.Repositories;
using CommercePortal.Domain.Entities;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework
{
    /// <summary>
    /// Represents the <see cref="Order"/> read repository.
    /// </summary>
    public class OrderReadRepository : EfReadRepository<Order, EfDbContext>, IOrderReadRepository
    {
    }
}