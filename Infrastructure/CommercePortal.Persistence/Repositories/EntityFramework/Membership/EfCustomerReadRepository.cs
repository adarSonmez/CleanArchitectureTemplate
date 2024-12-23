using CommercePortal.Application.Abstractions.Repositories.Membership;
using CommercePortal.Domain.Entities.Membership;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Membership;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Customer"/> read repository.
/// </summary>
public class EfCustomerReadRepository(EfDbContext context) : EfReadRepository<Customer>(context), ICustomerReadRepository
{
}