using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Identity;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Users.Queries.GetUserById;

/// <summary>
/// Handles the <see cref="GetProductByIdQueryRequest"/>.
/// </summary>
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQueryRequest, SingleResponse<UserDto?>>
{
    private readonly IUserService _userService;

    public GetUserByIdQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<SingleResponse<UserDto?>> Handle(GetUserByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<UserDto?>();
        var userDto = await _userService.GetByIdAsync(request.Id);

        response.SetData(userDto);

        return response;
    }
}