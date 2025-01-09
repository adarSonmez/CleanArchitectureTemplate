using CleanArchitectureTemplate.Application.Abstractions.Repositories.Files;
using CleanArchitectureTemplate.Domain.Entities.Files;
using CleanArchitectureTemplate.Persistence.Contexts;

namespace CleanArchitectureTemplate.Persistence.Repositories.EntityFramework.Files;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="IReportFileWriteRepository"/>.
/// </summary>
public class EfReportFileWriteRepository(EfDbContext context) : EfWriteRepository<ReportFile>(context), IReportFileWriteRepository
{
}