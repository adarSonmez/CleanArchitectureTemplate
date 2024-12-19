using System.Text.Json.Serialization;

namespace CommercePortal.Application.DTOs.Facebook;

/// <summary>
/// Represents the Facebook user info response data transfer object
/// </summary>
public class FacebookUserInfoResponseDTO
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;

    [JsonPropertyName("name")]
    public string? FullName { get; set; }
}