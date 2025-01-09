using CleanArchitectureTemplate.Application.Abstractions.Repositories.Membership;
using CleanArchitectureTemplate.Domain.Entities.Membership;
using CleanArchitectureTemplate.Persistence.Contexts;

namespace CleanArchitectureTemplate.Persistence.Repositories.EntityFramework.Membership;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Customer"/> read repository.
/// </summary>
public class EfCustomerReadRepository(EfDbContext context) : EfReadRepository<Customer>(context), ICustomerReadRepository
{
}