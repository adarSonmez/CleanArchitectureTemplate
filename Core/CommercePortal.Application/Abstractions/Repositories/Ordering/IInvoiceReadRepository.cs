using CommercePortal.Domain.Entities.Ordering;

namespace CommercePortal.Application.Abstractions.Repositories.Ordering;

/// <summary>
/// Represents the read repository interface for the <see cref="Invoice"/> entity.
/// </summary>
public interface IInvoiceReadRepository : IReadRepository<Invoice>
{
}