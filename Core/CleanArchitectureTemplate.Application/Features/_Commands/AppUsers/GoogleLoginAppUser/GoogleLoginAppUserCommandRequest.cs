﻿using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Commands.AppUsers.GoogleLoginAppUser;

/// <summary>
/// Represents a request for a user to log in using Google credentials
/// </summary>
/// <param name="Id">The ID of the user</param>
/// <param name="Email">The email of the user</param>
/// <param name="IdToken">The ID token of the user</param>
/// <param name="Provider">The provider of the user</param>
/// <param name="FirstName">The first name of the user</param>
/// <param name="LastName">The last name of the user</param>
/// <param name="PhotoUrl">The photo URL of the user</param>
public record GoogleLoginAppUserCommandRequest
(
    string Id,
    string Email,
    string IdToken,
    string Provider,
    string? FirstName,
    string? LastName,
    string? PhotoUrl
) : IRequest<GoogleLoginAppUserCommandResponse>;