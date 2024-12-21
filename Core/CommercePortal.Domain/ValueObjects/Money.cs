using CommercePortal.Domain.Constants.SmartEnums.Localizations;

namespace CommercePortal.Domain.ValueObjects;

/// <summary>
/// Represents a monetary value with an amount and a currency.
/// </summary>
public record Money
{
    #region Properties

    /// <summary>
    /// Gets the monetary amount.
    /// </summary>
    public decimal Amount { get; init; }

    /// <summary>
    /// Gets the currency of the monetary value.
    /// </summary>
    public Currency Currency { get; init; }

    #endregion Properties

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Money"/> record.
    /// </summary>
    /// <param name="amount">The monetary amount.</param>
    /// <param name="currency">The currency of the monetary value.</param>
    /// <exception cref="ArgumentException">Thrown when the amount is negative.</exception>
    public Money(decimal amount, Currency currency)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative.", nameof(amount));
        }

        Amount = amount;
        Currency = currency;
    }

    #endregion Constructors

    #region Methods

    /// <summary>
    /// Adds another <see cref="Money"/> value to this instance.
    /// </summary>
    /// <param name="other">The other monetary value.</param>
    /// <returns>A new <see cref="Money"/> record representing the sum.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the currencies do not match.</exception>
    public Money Add(Money other)
    {
        if (Currency != other.Currency)
        {
            throw new InvalidOperationException("Cannot add amounts with different currencies.");
        }

        return new Money(Amount + other.Amount, Currency);
    }

    /// <summary>
    /// Subtracts another <see cref="Money"/> value from this instance.
    /// </summary>
    /// <param name="other">The other monetary value.</param>
    /// <returns>A new <see cref="Money"/> record representing the difference.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the currencies do not match.</exception>
    public Money Subtract(Money other)
    {
        if (Currency != other.Currency)
        {
            throw new InvalidOperationException("Cannot subtract amounts with different currencies.");
        }

        return new Money(Amount - other.Amount, Currency);
    }

    public override string ToString()
    {
        return $"{Amount} {Currency.IsoCode}";
    }

    #endregion Methods
}