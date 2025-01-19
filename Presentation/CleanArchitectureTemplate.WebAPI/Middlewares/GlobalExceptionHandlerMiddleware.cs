using CleanArchitectureTemplate.Application.Common.Responses;
using Newtonsoft.Json;
using Serilog;
using System.Net;
using System.Net.Mime;

namespace CleanArchitectureTemplate.WebAPI.Middlewares;

/// <summary>
/// Middleware to handle unhandled exceptions globally.
/// </summary>
public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalExceptionHandlerMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    public GlobalExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Invokes the middleware to handle exceptions globally.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Log the exception details using Serilog
        Log.Error(exception, "An unhandled exception occurred");

        // Set the response status code and content type
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = MediaTypeNames.Application.Json;

        // Create a standardized error response
        var responseResult = new ResponseResult();
        responseResult.AddError("INTERNAL_SERVER_ERROR", "An unexpected error occurred. Please try again later.");

        // Serialize the error response to JSON and write to the response body
        var errorJson = JsonConvert.SerializeObject(responseResult);
        await context.Response.WriteAsync(errorJson);
    }
}