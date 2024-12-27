namespace CommercePortal.Application.Dtos.Files;

using CommercePortal.Domain.Constants.Enums;
using CommercePortal.Domain.MarkerInterfaces;

/// <summary>
/// Represents the report file data transfer object.
/// </summary>
public record ReportFileDto
(
    Guid Id,
    ReportType ReportType,
    FileDetailsDto? FileDetails
) : IDto;