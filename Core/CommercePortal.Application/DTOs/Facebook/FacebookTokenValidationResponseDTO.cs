using System.Text.Json.Serialization;

namespace CommercePortal.Application.DTOs.Facebook;

/// <summary>
/// Represents the Facebook token response data transfer object.
/// </summary>
public class FacebookTokenValidationResponseDTO
{
    public FacebookTokenValidationDataResponseDTO Data { get; set; }
}

public class FacebookTokenValidationDataResponseDTO
{
    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

    [JsonPropertyName("is_valid")]
    public bool IsValid { get; set; }
}