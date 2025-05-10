namespace CleanArchitectureTemplate.Application.Common.Responses;

/// <summary>
/// Represents the response constants.
/// </summary>
public static class ResponseConstants
{
    #region Response Codes

    public const string NotFoundErrorCode = "NOT_FOUND";
    public const string InvalidModelErrorCode = "MODEL_VALIDATION_FAILED";

    #endregion Response Codes

    #region Response Messages

    public const string NotFoundMessage = "Resource not found.";
    public const string NoItemsFoundMessage = "No items were found.";
    public const string SingleSuccessMessage = "Item retrieved successfully.";
    public const string PaginatedSuccessMessage = "Items retrieved successfully.";

    #endregion Response Messages

    #region Response Types

    public const string Success = "Success";
    public const string Error = "Error";
    public const string Warning = "Warning";

    #endregion Response Types
}