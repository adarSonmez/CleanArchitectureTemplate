using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL.ColumnWriters;

namespace CleanArchitectureTemplate.WebAPI.Configurations.ColumnWriters;

/// <summary>
/// A custom column writer for Serilog that extracts the "user_name" property from log events.
/// </summary>
public class UserNameColumnWriter : ColumnWriterBase
{
    public UserNameColumnWriter() : base(NpgsqlDbType.Varchar)
    {
    }

    /// <inheritdoc />
    public override object? GetValue(LogEvent logEvent, IFormatProvider? formatProvider = null)
    {
        return logEvent.Properties.TryGetValue("user_name", out var logEventProperty) ? logEventProperty.ToString() : null;
    }
}