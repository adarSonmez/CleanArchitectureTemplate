namespace CommercePortal.Application.RequestParameters;

/// <summary>
/// Represents pagination parameters for requests.
/// </summary>
public record Pagination
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
}