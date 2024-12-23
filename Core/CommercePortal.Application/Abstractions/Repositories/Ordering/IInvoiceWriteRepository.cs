using CommercePortal.Domain.Entities.Ordering;

namespace CommercePortal.Application.Abstractions.Repositories.Ordering;

/// <summary>
/// Represents the write repository interface for the <see cref="Invoice"/> entity.
/// </summary>
public interface IInvoiceWriteRepository : IWriteRepository<Invoice>
{
}