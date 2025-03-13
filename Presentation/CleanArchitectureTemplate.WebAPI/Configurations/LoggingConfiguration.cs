using CleanArchitectureTemplate.WebAPI.Configurations.ColumnWriters;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using Serilog.Sinks.PostgreSQL.ColumnWriters;

namespace CleanArchitectureTemplate.WebAPI.Configurations;

/// <summary>
/// Provides configuration for logging using Serilog.
/// </summary>
public static class LoggingConfiguration
{
    /// <summary>
    /// Configures logging for the application using Serilog.
    /// </summary>
    /// <param name="context">The host builder context.</param>
    /// <param name="configuration">The logger configuration.</param>
    public static void ConfigureSerilog(HostBuilderContext context, LoggerConfiguration configuration)
    {
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.Seq(context.Configuration["Logging:Seq:ServerUrl"]!)
            .WriteTo.PostgreSQL(
                context.Configuration.GetConnectionString("DefaultConnection")!,
                "Logs",
                columnOptions: new Dictionary<string, ColumnWriterBase>
                {
                    { "message", new RenderedMessageColumnWriter() },
                    { "message_template", new MessageTemplateColumnWriter() },
                    { "level", new LevelColumnWriter() },
                    { "timestamp", new TimestampColumnWriter() },
                    { "exception", new ExceptionColumnWriter() },
                    { "log_event", new LogEventSerializedColumnWriter() },
                    { "user_name", new UserNameColumnWriter() },
                    { "user_roles", new UserRolesColumnWriter() }
                },
                needAutoCreateTable: true
            );
    }

    /// <summary>
    /// Configures HTTP logging options.
    /// </summary>
    /// <param name="options">The HTTP logging options.</param>
    public static void ConfigureHttpLogging(HttpLoggingOptions options)
    {
        options.LoggingFields = HttpLoggingFields.All;
        options.RequestHeaders.Add("sec-ch-ua");
        options.MediaTypeOptions.AddText("application/javascript");
        options.RequestBodyLogLimit = 4096;
        options.ResponseBodyLogLimit = 4096;
        options.CombineLogs = true;
    }
}