using CommercePortal.Domain.Entities.Files;
using CommercePortal.Domain.MarkerInterfaces;

namespace CommercePortal.Application.Dtos.Files;

/// <summary>
/// Represents data transfer object for <see cref="InvoiceFile"/>
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="FileDetails">The file details data transfer object.</param>
public record InvoiceFileDto
(
    Guid Id,
    FileDetailsDto? FileDetails
) : IDto;