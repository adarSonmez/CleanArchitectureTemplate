using CommercePortal.Application.Abstractions.Repositories.Ordering;
using CommercePortal.Domain.Entities.Ordering;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Ordering;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Invoice"/> read repository.
/// </summary>
public class EfInvoiceReadRepository(EfDbContext context) : EfReadRepository<Invoice>(context), IInvoiceReadRepository
{
}