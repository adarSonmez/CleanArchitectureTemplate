namespace CleanArchitectureTemplate.WebAPI.Middlewares;

/// <summary>
/// Middleware to enforce Content Security Policy (CSP) headers for improved security.
/// </summary>
public class ContentSecurityPolicyMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContentSecurityPolicyMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="logger">The logger instance.</param>
    public ContentSecurityPolicyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Invokes the middleware to add Content Security Policy (CSP) headers to the response.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers.Append("Content-Security-Policy",
            "default-src 'self'; " +
            "script-src 'self' https://apis.google.com; " +
            "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com; " +
            "img-src 'self' data:; " +
            "font-src 'self' https://fonts.gstatic.com; " +
            "frame-src 'none'; " +
            "frame-ancestors 'none'; " +
            "object-src 'none'; " +
            "base-uri 'self'; " +
            "form-action 'self'; " +
            "connect-src 'self' https://api.example.com;"
        );

        context.Response.Headers.Append("X-Frame-Options", "DENY");

        await _next(context);
    }
}