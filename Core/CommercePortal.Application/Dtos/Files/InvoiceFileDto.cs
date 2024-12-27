using CommercePortal.Domain.MarkerInterfaces;

namespace CommercePortal.Application.Dtos.Files;

/// <summary>
/// Represents an invoice file DTO.
/// </summary>
public record InvoiceFileDto
(
    Guid Id,
    FileDetailsDto? FileDetails
) : IDto;