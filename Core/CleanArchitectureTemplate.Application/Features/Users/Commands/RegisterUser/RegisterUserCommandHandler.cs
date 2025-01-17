using AutoMapper;
using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Identity;
using CleanArchitectureTemplate.Domain.Common;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Users.Commands.RegisterUser;

/// <summary>
/// Represents a handler for the <see cref="RegisterUserCommandRequest"/>
/// </summary>
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, SingleResponse<UserDto?>>
{
    public readonly IMapper _mapper;
    public readonly IUserService _userService;

    public RegisterUserCommandHandler(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<SingleResponse<UserDto?>> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<UserDto?>();

        try
        {
            var userDto = await _userService.CreateAsync(request);

            BusinessRules.Run(("USR215761", BusinessRules.CheckDtoNull(userDto)));

            response.SetData(userDto);
        }
        catch (Exception ex)
        {
            response.AddError("USR633428", ex.Message);
        }

        return response;
    }
}