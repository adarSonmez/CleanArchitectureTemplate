using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace CleanArchitectureTemplate.Application.Common.Responses;

/// <summary>
/// Represents a response message with a type and optional code.
/// </summary>
public class ResponseResult : IActionResult
{
    /// <summary>
    /// Indicates if the response is successful (no errors).
    /// </summary>
    [JsonInclude]
    public bool IsSuccessful => !Messages.Any(m => m.MessageType == ResponseMessageType.Error);

    /// <summary>
    /// List of response messages (success, warnings, errors).
    /// </summary>
    [JsonInclude]
    public List<ResponseMessage> Messages { get; } = [];

    /// <summary>
    /// Adds a success message to the result.
    /// </summary>
    public void AddSuccess(string message) =>
        Messages.Add(ResponseMessage.Success(message));

    /// <summary>
    /// Adds a warning message to the result.
    /// </summary>
    public void AddWarning(string message) =>
        Messages.Add(ResponseMessage.Warning(message));

    /// <summary>
    /// Adds an error message to the result.
    /// </summary>
    public void AddError(string code, string message) =>
        Messages.Add(ResponseMessage.Error(code, message));

    /// <inheritdoc/>
    public async Task ExecuteResultAsync(ActionContext context)
    {
        var result = new ObjectResult(this)
        {
            StatusCode = IsSuccessful ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest
        };

        await result.ExecuteResultAsync(context);
    }
}