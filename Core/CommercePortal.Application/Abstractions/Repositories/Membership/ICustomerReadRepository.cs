using CommercePortal.Domain.Entities.Membership;

namespace CommercePortal.Application.Abstractions.Repositories.Membership;

/// <summary>
/// Represents the read repository interface for the <see cref="Customer"/> entity.
/// </summary>
public interface ICustomerReadRepository : IReadRepository<Customer>
{
}