using CleanArchitectureTemplate.Application.Common.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanArchitectureTemplate.Infrastructure.Filters;

/// <summary>
/// Represents a filter that validates the model state before executing an action.
/// </summary>
public class ValidationFilter : IAsyncActionFilter
{
    /// <inheritdoc/>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            var response = new ResponseResult();

            foreach (var error in errors)
            {
                response.AddError(ResponseConstants.InvalidModelErrorCode, error);
            }

            context.Result = response;
            return;
        }

        await next();
    }
}