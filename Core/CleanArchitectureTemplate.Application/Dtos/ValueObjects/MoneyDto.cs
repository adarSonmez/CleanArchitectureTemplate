using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Dtos.ValueObjects;

/// <summary>
/// Represents a monetary value with an amount and a currency.
/// </summary>
/// <param name="Amount"> The monetary amount.</param>
/// <param name="CurrencyIsoCode">The ISO code of the currency.</param>
public record MoneyDto
(
    decimal Amount = default,
    string CurrencyIsoCode = default!
) : IDto;