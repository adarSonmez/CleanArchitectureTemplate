using CleanArchitectureTemplate.WebAPI.Middlewares;

namespace CleanArchitectureTemplate.WebAPI.Extensions;

/// <summary>
/// Extension methods for adding middleware to the application pipeline.
/// </summary>
public static class ApplicationBuilderExtensions
{
    #region Middleware Extensions

    /// <summary>
    /// Adds the LoggingMiddleware to the application pipeline.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    /// <returns>The application builder.</returns>
    public static IApplicationBuilder UseLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingMiddleware>();
    }

    /// <summary>
    /// Adds the GlobalExceptionHandlerMiddleware to the application pipeline.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    /// <returns>The application builder.</returns>
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }

    #endregion Middleware Extensions
}