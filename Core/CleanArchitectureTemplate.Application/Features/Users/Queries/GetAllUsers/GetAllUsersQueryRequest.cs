using CleanArchitectureTemplate.Application.Attributes;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Identity;
using CleanArchitectureTemplate.Application.RequestParameters;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Users.Queries.GetAllUsers;

/// <summary>
/// Represents the request for getting all users by query.
/// </summary>
/// <param name="Pagination">The pagination parameters.</param>
[Cache(20, 60)]
public record GetAllUsersQueryRequest
(
    Pagination? Pagination
) : IRequest<PagedResponse<UserDto>>;