using CommercePortal.Application.Abstractions.Repositories.Ordering;
using CommercePortal.Domain.Entities.Ordering;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Ordering;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="Invoice"/> write repository.
/// </summary>
public class EfInvoiceWriteRepository(EfDbContext context) : EfWriteRepository<Invoice>(context), IInvoiceWriteRepository
{
}