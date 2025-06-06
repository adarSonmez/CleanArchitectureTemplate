﻿using CleanArchitectureTemplate.Application.Attributes;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Identity;
using MediatR;

namespace CleanArchitectureTemplate.Application.Features.Users.Queries.GetUserById;

/// <summary>
/// Represents the request for getting an individual user by ID.
/// </summary>
/// <param name="Id">The ID of the user to retrieve.</param>
[Cache(20, 60)]
public record GetUserByIdQueryRequest
(
    Guid Id
) : IRequest<SingleResponse<UserDto?>>;