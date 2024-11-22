using CommercePortal.Application.Repositories;
using CommercePortal.Domain.Entities;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework
{
    /// <summary>
    /// Represents the <see cref="Customer"/> write repository.
    /// </summary>
    public class CustomerWriteRepository : EfWriteRepository<Customer, EfDbContext>, ICustomerWriteRepository
    {
    }
}