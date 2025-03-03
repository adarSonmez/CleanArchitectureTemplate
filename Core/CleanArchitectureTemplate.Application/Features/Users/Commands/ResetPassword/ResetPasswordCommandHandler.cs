using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Users.Commands.ResetPassword
{
    /// <summary>
    /// Handles the password reset request.
    /// </summary>
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommandRequest, ResponseResult>
    {
        private readonly IUserService _userService;

        public ResetPasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResponseResult> Handle(ResetPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            await _userService.ResetPasswordAsync(request.UserId, request.Token, request.NewPassword);

            return new();
        }
    }
}