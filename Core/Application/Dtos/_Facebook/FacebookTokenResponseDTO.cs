using System.Text.Json.Serialization;

namespace CleanArchitectureTemplate.Application.DTOs.Facebook;

/// <summary>
/// Represents the Facebook token response data transfer object.
/// </summary>
public class FacebookTokenResponseDTO
{
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }

    [JsonPropertyName("token_type")]
    public string? TokenType { get; set; }
}