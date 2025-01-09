using CleanArchitectureTemplate.Domain.Entities.Ordering;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories.Ordering;

/// <summary>
/// Represents the read repository interface for the <see cref="Invoice"/> entity.
/// </summary>
public interface IInvoiceReadRepository : IReadRepository<Invoice>
{
}