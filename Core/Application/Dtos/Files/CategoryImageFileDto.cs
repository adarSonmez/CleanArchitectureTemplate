using CleanArchitectureTemplate.Domain.Entities.Files;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Dtos.Files;

/// <summary>
/// Represents data transfer object for <see cref="CategoryImageFile"/>
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="FileDetails">The file details data transfer object.</param>
/// <param name="CategoryId">The category identifier.</param>
public record CategoryImageFileDto
(
    Guid Id,
    FileDetailsDto? FileDetails,
    Guid CategoryId
) : IDto;