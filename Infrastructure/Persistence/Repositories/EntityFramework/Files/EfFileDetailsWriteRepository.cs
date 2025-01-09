using CleanArchitectureTemplate.Application.Abstractions.Repositories.Files;
using CleanArchitectureTemplate.Domain.Entities.Files;
using CleanArchitectureTemplate.Persistence.Contexts;

namespace CleanArchitectureTemplate.Persistence.Repositories.EntityFramework.Files;

/// <summary>
/// Represents EntityFramework implementation of the <see cref="IFileDetailsWriteRepository"/>.
/// </summary>
public class EfFileDetailsWriteRepository(EfDbContext context) : EfWriteRepository<FileDetails>(context), IFileDetailsWriteRepository
{
}