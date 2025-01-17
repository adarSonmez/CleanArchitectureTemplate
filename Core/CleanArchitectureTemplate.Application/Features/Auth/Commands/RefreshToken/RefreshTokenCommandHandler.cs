using AutoMapper;
using CleanArchitectureTemplate.Application.Abstractions.Repositories.Marketing;
using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Common.Responses;
using CleanArchitectureTemplate.Application.Dtos.Marketing;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Domain.Common;
using CleanArchitectureTemplate.Domain.Entities.Marketing;
using MediatR;
using System.Linq.Expressions;

namespace CleanArchitectureTemplate.Application.Features.Auth.Commands.RefreshToken;

/// <summary>
/// Handles the <see cref="RefreshTokenCommandRequest"/>.
/// </summary>
public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommandRequest, SingleResponse<TokenDto?>>
{
    private readonly IAuthenticationService _authenticationService;

    public RefreshTokenCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<SingleResponse<TokenDto?>> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse<TokenDto?>();
        try
        {
            var tokenDto = await _authenticationService.RefreshTokenAsync(request);
            BusinessRules.Run(("AUT512033", BusinessRules.CheckDtoNull(tokenDto)));
            response.SetData(tokenDto);
        }
        catch (Exception ex)
        {
            response.AddError("AUT380171", ex.Message);
        }
        return response;
    }
}