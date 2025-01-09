using CleanArchitectureTemplate.Domain.Entities.Files;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Dtos.Files;

/// <summary>
/// Represents data transfer object for <see cref="InvoiceFile"/>
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="FileDetails">The file details data transfer object.</param>
/// <param name="InvoiceId">The invoice identifier.</param>
public record InvoiceFileDto
(
    Guid Id,
    FileDetailsDto? FileDetails,
    Guid InvoiceId
) : IDto;