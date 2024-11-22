using CommercePortal.Application.Repositories;
using CommercePortal.Domain.Entities;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework
{
    /// <summary>
    /// Represents the <see cref="Customer"/> read repository.
    /// </summary>
    public class CustomerReadRepository : EfReadRepository<Customer, EfDbContext>, ICustomerReadRepository
    {
    }
}