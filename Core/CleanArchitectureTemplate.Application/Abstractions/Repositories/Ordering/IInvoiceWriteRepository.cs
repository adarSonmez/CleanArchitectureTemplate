using CleanArchitectureTemplate.Domain.Entities.Ordering;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories.Ordering;

/// <summary>
/// Represents the write repository interface for the <see cref="Invoice"/> entity.
/// </summary>
public interface IInvoiceWriteRepository : IWriteRepository<Invoice>
{
}