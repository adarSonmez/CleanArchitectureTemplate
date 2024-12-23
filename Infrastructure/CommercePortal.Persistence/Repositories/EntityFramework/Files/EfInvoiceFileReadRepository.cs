using CommercePortal.Application.Abstractions.Repositories.Files;
using CommercePortal.Domain.Entities.Files;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Files;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="IInvoiceFileReadRepository"/>.
/// </summary>
public class EfInvoiceFileReadRepository(EfDbContext context) : EfReadRepository<InvoiceFile>(context), IInvoiceFileReadRepository
{
}