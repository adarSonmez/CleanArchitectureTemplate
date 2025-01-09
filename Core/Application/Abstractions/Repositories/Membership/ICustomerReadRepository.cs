using CleanArchitectureTemplate.Domain.Entities.Membership;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories.Membership;

/// <summary>
/// Represents the read repository interface for the <see cref="Customer"/> entity.
/// </summary>
public interface ICustomerReadRepository : IReadRepository<Customer>
{
}