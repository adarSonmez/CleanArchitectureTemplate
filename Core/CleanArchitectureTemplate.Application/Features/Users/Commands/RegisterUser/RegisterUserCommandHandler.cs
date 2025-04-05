using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Identity;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Users.Commands.RegisterUser;

/// <summary>
/// Represents a handler for the <see cref="RegisterUserCommandRequest"/>
/// </summary>
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, SingleResponse<UserDto?>>
{
    public readonly IUserService _userService;

    public RegisterUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<SingleResponse<UserDto?>> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<UserDto?>();
        var userDto = await _userService.CreateAsync(request);
        response.SetData(userDto);

        return response;
    }
}