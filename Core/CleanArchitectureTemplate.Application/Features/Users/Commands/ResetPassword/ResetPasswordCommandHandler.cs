using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.Users.Commands.ResetPassword
{
    /// <summary>
    /// Handles the password reset request.
    /// </summary>
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommandRequest, SingleResponse<bool>>
    {
        private readonly IUserService _userService;

        public ResetPasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<SingleResponse<bool>> Handle(ResetPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new SingleResponse<bool>();

            var success = await _userService.ResetPasswordAsync(request.UserId, request.Token, request.NewPassword);

            if (!success)
            {
                throw new BadRequestException("Password reset failed.");
            }

            response.SetData(true);
            return response;
        }
    }
}