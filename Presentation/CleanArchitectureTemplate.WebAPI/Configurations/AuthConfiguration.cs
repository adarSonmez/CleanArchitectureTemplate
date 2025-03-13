using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace CleanArchitectureTemplate.WebAPI.Configurations;

/// <summary>
/// Provides configurations for authentication and authorization.
/// </summary>
public static class AuthConfiguration
{
    /// <summary>
    /// Configures authentication for the application.
    /// </summary>
    /// <param name="options">The authentication options.</param>
    public static void ConfigureAuthentication(AuthenticationOptions options)
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }

    /// <summary>
    /// Configures authorization policies for the application.
    /// </summary>
    /// <param name="options">The authorization options.</param>
    public static void ConfigureAuthorization(AuthorizationOptions options)
    {
        options.AddPolicy("AdminOrManageUsers", policy =>
            policy.RequireAssertion(context =>
                context.User.IsInRole("Admin") ||
                context.User.HasClaim("CanManageUsers", "true")
            ));

        options.AddPolicy("StoreManageOrders", policy =>
            policy.RequireAssertion(context =>
                context.User.IsInRole("Store") &&
                context.User.HasClaim("CanManageOrders", "true")
            ));

        options.AddPolicy("LoyalCustomer", policy =>
            policy.RequireAssertion(context =>
                context.User.IsInRole("Customer") &&
                context.User.Claims.Any(c =>
                    c.Type == "LoyaltyLevel" && c.Value == "Gold")
            ));
    }

    /// <summary>
    /// Configures JWT bearer authentication for the application.
    /// </summary>
    /// <param name="options">The JWT bearer options.</param>
    /// <param name="configuration">The configuration.</param>
    public static void ConfigureJwtBearer(JwtBearerOptions options, IConfiguration configuration)
    {
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Cookies["AccessToken"];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                }
                return Task.CompletedTask;
            }
        };

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            LifetimeValidator = (before, expires, token, param) => expires > DateTime.UtcNow,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!)),
            ClockSkew = TimeSpan.Zero,
            NameClaimType = ClaimTypes.Name,
            RoleClaimType = ClaimTypes.Role
        };
    }
}