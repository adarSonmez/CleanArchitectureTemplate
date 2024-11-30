using CommercePortal.Application.Repositories;
using CommercePortal.Domain.Entities;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework
{
    /// <summary>
    /// Represents EntityFramework implementation of the <see cref="Customer"/> write repository.
    /// </summary>
    public class EfCustomerWriteRepository(EfDbContext context) : EfWriteRepository<Customer>(context), ICustomerWriteRepository
    {
    }
}