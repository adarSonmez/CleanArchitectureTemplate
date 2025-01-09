using CleanArchitectureTemplate.Application.Abstractions.Repositories.Ordering;
using CleanArchitectureTemplate.Domain.Entities.Ordering;
using CleanArchitectureTemplate.Persistence.Contexts;

namespace CleanArchitectureTemplate.Persistence.Repositories.EntityFramework.Ordering;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Invoice"/> read repository.
/// </summary>
public class EfInvoiceReadRepository(EfDbContext context) : EfReadRepository<Invoice>(context), IInvoiceReadRepository
{
}