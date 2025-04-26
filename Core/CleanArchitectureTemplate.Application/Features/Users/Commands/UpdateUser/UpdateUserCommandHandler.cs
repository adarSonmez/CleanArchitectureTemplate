using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Identity;
using CleanArchitectureTemplate.Application.Exceptions;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Users.Commands.UpdateUser;

/// <summary>
/// Represents a handler for the <see cref="UpdateUserCommandRequest"/>
/// </summary>
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, SingleResponse<UserDto?>>
{
    public readonly IUserService _userService;
    private readonly IUserContextService _userContextService;

    public UpdateUserCommandHandler(IUserService userService, IUserContextService userContextService)
    {
        _userService = userService;
        _userContextService = userContextService;
    }

    public async Task<SingleResponse<UserDto?>> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
    {
        if (!_userContextService.IsAdminOrSelf(request.Id))
            throw new ForbiddenException();

        var response = new SingleResponse<UserDto?>();
        var userDto = await _userService.UpdateAsync(request);
        response.SetData(userDto);

        return response;
    }
}