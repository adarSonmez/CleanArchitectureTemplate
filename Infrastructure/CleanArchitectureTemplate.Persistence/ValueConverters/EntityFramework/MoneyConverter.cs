using CleanArchitectureTemplate.Domain.Constants.SmartEnums.Localizations;
using CleanArchitectureTemplate.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CleanArchitectureTemplate.Persistence.ValueConverters.EntityFramework;

/// <summary>
/// Represents a value converter for <see cref="Money"/> values.
/// </summary>
public class MoneyConverter : ValueConverter<Money, string>
{
    public MoneyConverter()
        : base(
            money => $"{money.Amount}:{money.Currency.IsoCode}",
            moneyStr => ConvertToMoney(moneyStr)
        )
    {
    }

    /// <summary>
    /// Converts a string representation of money to a <see cref="Money"/> value.
    /// </summary>
    /// <param name="moneyStr">The string representation of money.</param>
    /// <returns>A new <see cref="Money"/> value.</returns>
    private static Money ConvertToMoney(string moneyStr)
    {
        var parts = moneyStr.Split(':');
        var amount = decimal.Parse(parts[0]);
        var currency = Currency.FromIsoCode(parts[1]);
        return new Money(amount, currency);
    }
}