using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Identity;
using CleanArchitectureTemplate.Application.Exceptions;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Users.Queries.GetUserById;

/// <summary>
/// Handles the <see cref="GetUserByIdQueryRequest"/>.
/// </summary>
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQueryRequest, SingleResponse<UserDto?>>
{
    private readonly IUserService _userService;
    private readonly IUserContextService _userContextService;

    public GetUserByIdQueryHandler(IUserService userService, IUserContextService userContextService)
    {
        _userService = userService;
        _userContextService = userContextService;
    }

    public async Task<SingleResponse<UserDto?>> Handle(GetUserByIdQueryRequest request, CancellationToken cancellationToken)
    {
        if (!_userContextService.IsAdminOrSelf(request.Id))
            throw new ForbiddenException();

        var response = new SingleResponse<UserDto?>();
        var userDto = await _userService.GetByIdAsync(request.Id);

        response.SetData(userDto);
        return response;
    }
}