using CommercePortal.Application.Repositories;
using CommercePortal.Domain.Entities;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework
{
    /// <summary>
    /// Represents the <see cref="Order"/> write repository.
    /// </summary>
    public class OrderWriteRepository : EfWriteRepository<Order, EfDbContext>, IOrderWriteRepository
    {
    }
}