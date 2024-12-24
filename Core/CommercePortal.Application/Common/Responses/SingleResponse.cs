using CommercePortal.Application.Common.Constants.StringConstants;

namespace CommercePortal.Application.Common.Responses;

/// <summary>
/// Response for a single object.
/// </summary>
public class SingleResponse<T> : ResponseResult
{
    public T? Data { get; private set; }

    public void SetData(T data, string? successMessage)
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