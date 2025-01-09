using CleanArchitectureTemplate.Domain.Common;
using CleanArchitectureTemplate.Domain.Constants.SmartEnums.Localizations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CleanArchitectureTemplate.Persistence.ValueConverters.EntityFramework;

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