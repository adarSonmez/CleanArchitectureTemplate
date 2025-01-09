namespace CleanArchitectureTemplate.Application.Common.Constants.StringConstants;

/// <summary>
/// Represents the response constants.
/// </summary>
public static class ResponseConstants
{
    #region Response Codes

    public const string NotFoundErrorCode = "APP-404";

    #endregion Response Codes

    #region Response Messages

    public const string NotFoundMessage = "Resource not found.";
    public const string NoItemsFoundMessage = "No items were found.";
    public const string SingleSuccessMessage = "Item retrieved successfully.";
    public const string PaginatedSuccessMessage = "Items retrieved successfully.";

    #endregion Response Messages
}