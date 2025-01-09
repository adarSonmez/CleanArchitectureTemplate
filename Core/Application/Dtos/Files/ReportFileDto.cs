using CleanArchitectureTemplate.Domain.Constants.Enums;
using CleanArchitectureTemplate.Domain.Entities.Files;
using CleanArchitectureTemplate.Domain.MarkerInterfaces;

namespace CleanArchitectureTemplate.Application.Dtos.Files;

/// <summary>
/// Represents data transfer object for <see cref="ReportFile"/>
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