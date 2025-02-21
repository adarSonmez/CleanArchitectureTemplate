using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.DTOs;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.FacebookLogin;

/// <summary>
/// Represents a request for a user to log in using Facebook credentials
/// </summary>
/// <param name="AccessToken">The access token of the user</param>
/// <param name="Provider">The provider of the user</param>
public record FacebookLoginCommandRequest
(
    string AccessToken,
    string Provider
) : IRequest<SingleResponse<bool>>;