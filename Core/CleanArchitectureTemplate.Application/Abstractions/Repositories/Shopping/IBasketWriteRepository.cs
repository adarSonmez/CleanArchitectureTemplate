using CleanArchitectureTemplate.Domain.Entities.Shopping;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories.Shopping;

/// <summary>
/// Represents the write repository interface for the <see cref="Basket"/> entity.
/// </summary>
public interface IBasketWriteRepository : IWriteRepository<Basket>
{
}