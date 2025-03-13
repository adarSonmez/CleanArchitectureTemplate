using Microsoft.AspNetCore.Cors.Infrastructure;

namespace CleanArchitectureTemplate.WebAPI.Configurations;

/// <summary>
/// Provides configuration for CORS.
/// </summary>
public static class CorsConfiguration
{
    /// <summary>
    /// Configures CORS to allow requests from any origin, method, and header.
    /// </summary>
    /// <param name="options">The CORS options.</param>
    public static void ConfigureCors(CorsOptions options)
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    }
}