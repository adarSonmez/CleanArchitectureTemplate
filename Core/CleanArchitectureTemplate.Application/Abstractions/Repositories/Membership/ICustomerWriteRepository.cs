using CleanArchitectureTemplate.Domain.Entities.Membership;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories.Membership;

/// <summary>
/// Represents the write repository interface for the <see cref="Customer"/> entity.
/// </summary>
public interface ICustomerWriteRepository : IWriteRepository<Customer>
{
}