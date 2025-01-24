using CleanArchitectureTemplate.Domain.Entities.Shopping;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;

/// <summary>
/// Represents the write repository interface for the <see cref="BasketItem"/> entity.
/// </summary>
public interface IBasketItemWriteRepository : IWriteRepository<BasketItem>
{
}