using CleanArchitectureTemplate.Domain.MarkerInterfaces;
using System.Text.Json.Serialization;

namespace CleanArchitectureTemplate.Application.DTOs.Facebook;

/// <summary>
/// Represents the Facebook user info data transfer object
/// </summary>
public record FacebookUserInfoDto
(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("first_name")] string FirstName,
    [property: JsonPropertyName("last_name")] string LastName
) : IDto;