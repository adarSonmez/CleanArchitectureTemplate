using CommercePortal.Application.Repositories.Customers;
using CommercePortal.Domain.Entities;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Customers;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Customer"/> write repository.
/// </summary>
public class EfCustomerWriteRepository(EfDbContext context) : EfWriteRepository<Customer>(context), ICustomerWriteRepository
{
}