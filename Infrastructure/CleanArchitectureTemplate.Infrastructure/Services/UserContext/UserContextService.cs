using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Domain.Constants.StringConstants;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CleanArchitectureTemplate.Infrastructure.Services.UserContext;

/// <summary>
/// Implementation of IUserContextService to retrieve the current user.
/// </summary>
public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc/>
    public string? GetUserName()
    {
        return _httpContextAccessor.HttpContext?.User?.Identity?.Name;
    }

    /// <inheritdoc/>
    public Guid? GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(userId, out var parsedId) ? parsedId : null;
    }

    /// <inheritdoc/>
    public bool IsAdminOrSelf(Guid userId)
    {
        return IsInRole(UserRoles.Admin) || IsCurrentUser(userId);
    }

    /// <inheritdoc/>
    public bool IsInRole(string role)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        return httpContext?.User?.IsInRole(role) ?? false;
    }

    /// <inheritdoc/>
    public bool IsCurrentUser(Guid userId)
    {
        var currentUserId = GetUserId();
        return currentUserId.HasValue && currentUserId.Value == userId;
    }

    /// <inheritdoc/>
    public List<string> GetUserRoles()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        return httpContext?.User?.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList() ?? new List<string>();
    }

    /// <inheritdoc/>
    public bool HasAnyRole(params string[] roles)
    {
        var userRoles = GetUserRoles();
        return userRoles.Any(role => roles.Contains(role));
    }

    /// <inheritdoc/>
    public bool IsAuthenticated()
    {
        return _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}