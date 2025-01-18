using Serilog.Context;
using System.Security.Claims;

namespace CleanArchitectureTemplate.WebAPI.Middlewares;

/// <summary>
/// Middleware to enrich Serilog logs with additional context information about the user and request.
/// </summary>
public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoggingMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Invokes the middleware to add user and request context to the Serilog log.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        var userName = context.User?.Identity?.Name ?? "anonymous";
        LogContext.PushProperty("user_name", userName);

        var userRoles = context.User?.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList() ?? [];
        LogContext.PushProperty("user_roles", userRoles);

        await _next(context);
    }
}

/// <summary>
/// Extension method to add the <see cref="LoggingMiddleware"/> to the application's middleware pipeline.
/// </summary>
public static class LoggingMiddlewareExtensions
{
    /// <summary>
    /// Adds the LoggingMiddleware to the application pipeline.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    /// <returns>The application builder.</returns>
    public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingMiddleware>();
    }
}