using MediatR;

namespace CommercePortal.Application.Features.Commands.AppUsers.GoogleLoginAppUser;

/// <summary>
/// Represents a handler for the <see cref="GoogleLoginAppUserCommandRequest"/>
/// </summary>
public class GoogleLoginAppUserCommandHandler : IRequestHandler<GoogleLoginAppUserCommandRequest, GoogleLoginAppUserCommandResponse>
{
    //public readonly UserManager<DomainUser> _userManager;
    //public readonly IConfiguration _configuration;
    //private readonly ITokenHandler _tokenHandler;

    //public GoogleLoginAppUserCommandHandler(UserManager<DomainUser> userManager, IConfiguration configuration, ITokenHandler tokenHandler)
    //{
    //    _userManager = userManager;
    //    _configuration = configuration;
    //    _tokenHandler = tokenHandler;
    //}

    public async Task<GoogleLoginAppUserCommandResponse> Handle(GoogleLoginAppUserCommandRequest request, CancellationToken cancellationToken)
    {
        //    var settings = new GoogleJsonWebSignature.ValidationSettings
        //    {
        //        Audience = [_configuration["OAuth:Google:ClientId"]]
        //    };

        //    var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);

        //    //  This model is used to store the user's information to the AspNetUserLogins table
        //    var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);

        //    var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        //    // If the user does not exist, create a new user
        //    if (user == null)
        //    {
        //        user = await _userManager.FindByEmailAsync(payload.Email);
        //        if (user == null)
        //        {
        //            user = new AppUser
        //            {
        //                Email = payload.Email,
        //                UserName = payload.Email,
        //                FullName = $"{request.FirstName} {request.LastName}",
        //            };

        //            var result = await _userManager.CreateAsync(user);
        //            if (!result.Succeeded)
        //            {
        //                throw new Exception("Failed to create user");
        //            }
        //        }

        //        var loginResult = await _userManager.AddLoginAsync(user, info);
        //        if (!loginResult.Succeeded)
        //        {
        //            throw new Exception("Failed to add login");
        //        }
        //    }

        //    var token = _tokenHandler.GenerateToken(user.Id);
        //    return new GoogleLoginAppUserCommandResponse(token);

        return null;
    }
}