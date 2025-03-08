using System.Text.Json.Serialization;

namespace CleanArchitectureTemplate.Application.Common.Responses;

/// <summary>
/// Response for a single object.
/// </summary>
public class SingleResponse<T> : ResponseResult
{
    [JsonInclude]
    public T? Data { get; private set; }

    public void SetData(T? data, string? successMessage = null)
    {
        if (data is null)
        {
            AddError(ResponseConstants.NotFoundErrorCode, ResponseConstants.NotFoundMessage);
        }
        else
        {
            Data = data;
            AddSuccess(successMessage ?? ResponseConstants.SingleSuccessMessage);
        }
    }
}