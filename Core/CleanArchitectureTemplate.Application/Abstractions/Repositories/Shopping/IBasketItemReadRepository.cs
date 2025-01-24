using CleanArchitectureTemplate.Domain.Entities.Shopping;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;

/// <summary>
/// Represents the read repository interface for the <see cref="BasketItem"/> entity.
/// </summary>
public interface IBasketItemReadRepository : IReadRepository<BasketItem>
{
}