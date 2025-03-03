using CleanArchitectureTemplate.Application.Common.Responses;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Users.Commands.ForgotPassword;

/// <summary>
/// Represents a request to initiate a password reset process.
/// </summary>
/// <param name="Email">The email of the user requesting a password reset</param>
public record ForgotPasswordCommandRequest(string Email) : IRequest<ResponseResult>;