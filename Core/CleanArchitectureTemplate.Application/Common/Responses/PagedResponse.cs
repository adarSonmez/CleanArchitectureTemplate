using System.Text.Json.Serialization;

namespace CleanArchitectureTemplate.Application.Common.Responses;

/// <summary>
/// Response for a paginated collection.
/// </summary>
public class PagedResponse<T> : ResponseResult
{
    /// <summary>
    /// Gets the enumerable data.
    /// </summary>
    [JsonInclude]
    public IEnumerable<T> Data { get; private set; } = [];

    /// <summary>
    /// Gets the current page.
    /// </summary>
    [JsonInclude]
    public int CurrentPage { get; private set; }

    /// <summary>
    /// Gets the page size.
    /// </summary>
    [JsonInclude]
    public int PageSize { get; private set; }

    /// <summary>
    /// Gets the count of items.
    /// </summary>
    [JsonInclude]
    public int TotalCount { get; private set; }

    /// <summary>
    /// Gets the total pages.
    /// </summary>
    [JsonInclude]
    public int TotalPages => TotalCount == 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);

    /// <summary>
    /// Sets the data.
    /// </summary>
    public void SetData(IEnumerable<T> data, int? page = null, int? pageSize = null, string? successMessage = null)
    {
        Data = data ?? [];
        TotalCount = Data.Count();
        CurrentPage = page ?? 1;
        PageSize = pageSize ?? TotalCount;

        if (TotalCount == 0)
        {
            AddWarning(ResponseConstants.NoItemsFoundMessage);
        }
        else
        {
            AddSuccess(successMessage ?? ResponseConstants.PaginatedSuccessMessage);
        }
    }
}