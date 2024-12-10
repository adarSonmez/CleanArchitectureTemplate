using CommercePortal.Application.Repositories.Files;
using CommercePortal.Domain.Entities;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework.Files;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="InvoiceFile"/> write repository.
/// </summary>
public class EfInvoiceFileWriteRepository(EfDbContext context) : EfWriteRepository<InvoiceFile>(context), IInvoiceFileWriteRepository
{
}