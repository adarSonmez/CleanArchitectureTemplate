namespace CommercePortal.Domain.ValueObjects;

/// <summary>
/// Represents a physical address.
/// </summary>
/// <param name="PostalCode">The postal code.</param>
/// <param name="City">The city.</param>
/// <param name="Country">The country.</param>
public record Address(string PostalCode, string City, string Country)
{
    /// <summary>
    /// Formats the address as a single-line string.
    /// </summary>
    /// <returns>A single-line string representation of the address.</returns>
    public override string ToString()
    {
        return $"{PostalCode}, {City}, {Country}";
    }
}