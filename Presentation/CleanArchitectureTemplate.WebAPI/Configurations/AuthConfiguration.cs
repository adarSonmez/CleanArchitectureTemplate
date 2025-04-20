using CleanArchitectureTemplate.Application.Constants.StringConstants;
using CleanArchitectureTemplate.Application.Options;
using CleanArchitectureTemplate.Domain.Constants.StringConstants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
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
        options.AddPolicy("AdminOrCreateUsers", policy =>
            policy.RequireAssertion(context =>
                context.User.IsInRole(UserRoles.Admin) ||
                context.User.HasClaim(ClaimNames.Permission, Permissions.UserCreate)
            ));

        options.AddPolicy("StoreOrUpdateOrders", policy =>
            policy.RequireAssertion(context =>
                context.User.IsInRole(UserRoles.Store) &&
                context.User.HasClaim(ClaimNames.Permission, Permissions.OrderUpdate)
            ));

        options.AddPolicy("LoyalCustomer", policy =>
            policy.RequireAssertion(context =>
                context.User.IsInRole(UserRoles.Customer) &&
                context.User.HasClaim(ClaimNames.LoyaltyLevel, "Gold")
            ));
    }

    /// <summary>
    /// Configures JWT bearer authentication for the application.
    /// </summary>
    /// <param name="options">The JWT bearer options.</param>
    /// <param name="configuration">The configuration.</param>
    public static void ConfigureJwtBearer(JwtBearerOptions options, IServiceCollection services)
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var jwtOptions = scope.ServiceProvider.GetRequiredService<IOptions<JwtOptions>>().Value;

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
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            LifetimeValidator = (before, expires, token, param) => expires > DateTime.UtcNow,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
            ClockSkew = TimeSpan.Zero,
            NameClaimType = ClaimTypes.Name,
            RoleClaimType = ClaimTypes.Role
        };
    }
}