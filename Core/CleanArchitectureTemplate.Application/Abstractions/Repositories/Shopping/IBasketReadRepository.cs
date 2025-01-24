using CleanArchitectureTemplate.Domain.Entities.Shopping;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;

/// <summary>
/// Represents the read repository interface for the <see cref="Basket"/> entity.
/// </summary>
public interface IBasketReadRepository : IReadRepository<Basket>
{
}