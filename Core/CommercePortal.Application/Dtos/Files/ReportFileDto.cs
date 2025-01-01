namespace CommercePortal.Application.Dtos.Files;

using CommercePortal.Domain.Constants.Enums;
using CommercePortal.Domain.MarkerInterfaces;

/// <summary>
/// Represents the report file data transfer object.
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="ReportType">The type of the report.</param>
/// <param name="FileDetails">The file details data transfer object.</param>
public record ReportFileDto
(
    Guid Id,
    ReportType ReportType,
    FileDetailsDto? FileDetails
) : IDto;