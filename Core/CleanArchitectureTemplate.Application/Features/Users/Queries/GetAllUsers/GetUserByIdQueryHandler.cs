using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Identity;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Users.Queries.GetAllUsers;

/// <summary>
/// Handles the <see cref="GetAllUsersQueryRequest"/>.
/// </summary>
public class GetUserByIdQueryHandler : IRequestHandler<GetAllUsersQueryRequest, PagedResponse<UserDto>>
{
    private readonly IUserService _userService;

    public GetUserByIdQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<PagedResponse<UserDto>> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new PagedResponse<UserDto>();

        var (data, totalCount) = await _userService.GetAllPaginatedAsync(request.Pagination);
        response.SetData(data, totalCount, request.Pagination?.Page, request.Pagination?.Size);

        return response;
    }
}