using CommercePortal.Application.Repositories.Files;
using CommercePortal.Domain.Entities;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Files;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="InvoiceFile"/> read repository.
/// </summary>
public class EfInvoiceFileReadRepository(EfDbContext context) : EfReadRepository<InvoiceFile>(context), IInvoiceFileReadRepository
{
}