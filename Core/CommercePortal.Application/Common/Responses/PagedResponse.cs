using CommercePortal.Application.Common.Constants.StringConstants;

namespace CommercePortal.Application.Common.Responses;

/// <summary>
/// Response for a paginated collection.
/// </summary>
public class PagedResponse<T> : ResponseResult
{
    /// <summary>
    /// Gets the enumerable data.
    /// </summary>
    public IEnumerable<T> Data { get; private set; } = [];

    /// <summary>
    /// Gets the current page.
    /// </summary>
    public int CurrentPage { get; private set; }

    /// <summary>
    /// Gets the page size.
    /// </summary>
    public int PageSize { get; private set; }

    /// <summary>
    /// Gets the count of items.
    /// </summary>
    public int TotalCount { get; private set; }

    /// <summary>
    /// Gets the total pages.
    /// </summary>
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    /// <summary>
    /// Sets the data.
    /// </summary>
    public void SetData(IEnumerable<T> data, int page, int pageSize, int totalCount, string? successMessage)
    {
        Data = data ?? [];
        CurrentPage = page;
        PageSize = pageSize;
        TotalCount = totalCount;

        if (!Data.Any())
        {
            AddWarning(ResponseConstants.NoItemsFoundMessage);
        }
        else
        {
            AddSuccess(successMessage ?? ResponseConstants.PaginatedSuccessMessage);
        }
    }
}