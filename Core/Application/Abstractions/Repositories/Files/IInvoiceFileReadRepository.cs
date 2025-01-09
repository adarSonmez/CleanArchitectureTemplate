using CleanArchitectureTemplate.Domain.Entities.Files;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories.Files;

/// <summary>
/// Represents the read repository interface for the <see cref="InvoiceFile"/> entity.
/// </summary>
public interface IInvoiceFileReadRepository : IReadRepository<InvoiceFile>
{
}