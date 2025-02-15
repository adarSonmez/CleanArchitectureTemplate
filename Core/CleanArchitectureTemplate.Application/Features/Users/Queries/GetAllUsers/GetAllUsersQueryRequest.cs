using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Identity;
using CleanArchitectureTemplate.Application.RequestParameters;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Users.Queries.GetAllUsers;

/// <summary>
/// Represents the request for getting all users by query.
/// </summary>
/// <param name="Pagination">The pagination parameters.</param>
public record GetAllUsersQueryRequest
(
    Pagination? Pagination
) : IRequest<PagedResponse<UserDto>>;