using CommercePortal.Domain.Common;
using CommercePortal.Domain.Constants.SmartEnums.Localizations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CommercePortal.Persistence.ValueConverters.EntityFramework;

/// <summary>
/// Represents a value converter for <see cref="Country"/> values.
/// </summary>
public class CountryConverter : ValueConverter<Country, string>
{
    public CountryConverter()
        : base(
            fileExt => fileExt.Name,
            extStr => Enumeration.FromName<Country>(extStr)
        )
    {
    }
}