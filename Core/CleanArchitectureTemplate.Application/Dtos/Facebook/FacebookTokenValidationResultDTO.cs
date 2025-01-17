using System.Text.Json.Serialization;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Dtos.Facebook;

/// <summary>
/// Represents the Facebook token response data transfer object.
/// </summary>
public record FacebookTokenValidationResultDto
(
    [property: JsonPropertyName("data")] FacebookTokenDataDto Data
) : IDto;