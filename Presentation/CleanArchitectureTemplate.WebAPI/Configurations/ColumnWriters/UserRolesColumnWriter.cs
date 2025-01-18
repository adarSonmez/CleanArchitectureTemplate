using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL.ColumnWriters;

namespace CleanArchitectureTemplate.WebAPI.Configurations.ColumnWriters;

/// <summary>
/// A custom column writer for Serilog that extracts the "user_roles" property from log events.
/// </summary>
public class UserRolesColumnWriter : ColumnWriterBase
{
    public UserRolesColumnWriter() : base(NpgsqlDbType.Json)
    {
    }

    /// <inheritdoc />
    public override object? GetValue(LogEvent logEvent, IFormatProvider? formatProvider = null)
    {
        return logEvent.Properties.TryGetValue("user_roles", out var logEventProperty) ? logEventProperty.ToString() : null;
    }
}