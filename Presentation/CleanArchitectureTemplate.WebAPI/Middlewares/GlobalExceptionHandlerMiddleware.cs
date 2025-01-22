using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Domain.Exceptions;
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

    /// <summary>
    /// Handles exceptions by mapping them to appropriate HTTP responses and logs the error appropriately.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    /// <param name="exception">The exception to handle.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Map the exception to HTTP status, error code, and message
        var (statusCode, errorCode, message) = exception switch
        {
            BadRequestException ex => (ex.StatusCode, ex.ErrorCode, ex.Message),
            ConflictException ex => (ex.StatusCode, ex.ErrorCode, ex.Message),
            ExpectationFailedException ex => (ex.StatusCode, ex.ErrorCode, ex.Message),
            FailedDependencyException ex => (ex.StatusCode, ex.ErrorCode, ex.Message),
            ForbiddenException ex => (ex.StatusCode, ex.ErrorCode, ex.Message),
            LockedException ex => (ex.StatusCode, ex.ErrorCode, ex.Message),
            MissingImpementationException ex => (ex.StatusCode, ex.ErrorCode, ex.Message),
            NotAcceptableException ex => (ex.StatusCode, ex.ErrorCode, ex.Message),
            NotFoundException ex => (ex.StatusCode, ex.ErrorCode, ex.Message),
            OperationFailedException ex => (ex.StatusCode, ex.ErrorCode, ex.Message),
            PaymentRequiredException ex => (ex.StatusCode, ex.ErrorCode, ex.Message),
            PreconditionFailedException ex => (ex.StatusCode, ex.ErrorCode, ex.Message),
            RateLimitExceededException ex => (ex.StatusCode, ex.ErrorCode, ex.Message),
            RequestedRangeNotSatisfiableException ex => (ex.StatusCode, ex.ErrorCode, ex.Message),
            RequestTimeoutException ex => (ex.StatusCode, ex.ErrorCode, ex.Message),
            ServiceUnavailableException ex => (ex.StatusCode, ex.ErrorCode, ex.Message),
            TooManyRequestsException ex => (ex.StatusCode, ex.ErrorCode, ex.Message),
            UnauthorizedException ex => (ex.StatusCode, ex.ErrorCode, ex.Message),
            UnavailableForLegalReasonsException ex => (ex.StatusCode, ex.ErrorCode, ex.Message),
            Domain.Exceptions.ValidationFailedException ex => (ex.StatusCode, ex.ErrorCode, ex.Message),
            _ => (HttpStatusCode.InternalServerError, "INTERNAL_SERVER_ERROR", "An unexpected error occurred. Please try again later.")
        };

        // Log the exception
        if (statusCode == HttpStatusCode.InternalServerError)
        {
            Log.Error(exception, "An unhandled exception occurred");
        }
        else
        {
            Log.Warning("Handled exception: {Message}", exception.Message);
        }

        // Set the response status code and content type
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = MediaTypeNames.Application.Json;

        // Create a standardized error response
        var response = new ResponseResult();
        response.AddError(errorCode, message);

        // Serialize the error response and write it to the response body
        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}