using CleanArchitectureTemplate.Domain.Entities.Files;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Dtos.Files;

/// <summary>
/// Represents data transfer object for <see cref="ProductImageFile"/>
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="ProductId">The product identifier.</param>
/// <param name="IsPrimary">The flag indicating whether the image is primary.</param>
/// <param name="FileDetails">The file details data transfer object.</param>
public record ProductImageFileDto
(
    Guid Id = default,
    Guid ProductId = default,
    bool IsPrimary = default,
    FileDetailsDto? FileDetails = default
) : IDto;