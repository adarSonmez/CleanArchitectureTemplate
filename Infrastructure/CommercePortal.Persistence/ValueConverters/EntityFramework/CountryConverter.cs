using CommercePortal.Domain.Common;
using CommercePortal.Domain.Constants.SmartEnums.Files;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CommercePortal.Persistence.ValueConverters.EntityFramework;

/// <summary>
/// Represents a value converter for <see cref="FileExtension"/> values.
/// </summary>
public class FileExtensionConverter : ValueConverter<FileExtension, string>
{
    public FileExtensionConverter()
        : base(
            fileExt => fileExt.Name,
            extStr => Enumeration.FromName<FileExtension>(extStr)
        )
    {
    }
}