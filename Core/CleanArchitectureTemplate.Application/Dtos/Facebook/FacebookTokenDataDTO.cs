using CleanArchitectureTemplate.Domain.MarkerInterfaces;
using System.Text.Json.Serialization;

namespace CleanArchitectureTemplate.Application.Dtos.Facebook;

/// <summary>
/// Represents the Facebook token data "data transfer object".
/// </summary>
public record FacebookTokenDataDto
(
    [property: JsonPropertyName("is_valid")] bool IsValid,
    [property: JsonPropertyName("app_id")] string AppId,
    [property: JsonPropertyName("user_id")] string UserId,
    [property: JsonPropertyName("expires_at")] int ExpiresAt
) : IDto;