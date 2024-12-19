using CommercePortal.Application.Abstractions.Token;
using CommercePortal.Application.DTOs.Facebook;
using CommercePortal.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace CommercePortal.Application.Features.Commands.AppUsers.FacebookLoginAppUser
{
    /// <summary>
    /// Represents a handler for the <see cref="FacebookLoginAppUserCommandRequest"/>.
    /// Handles the logic of verifying a Facebook Auth token, validating and/or creating a corresponding user,
    /// and returning a token for the logged-in user.
    /// </summary>
    public class FacebookLoginAppUserCommandHandler : IRequestHandler<FacebookLoginAppUserCommandRequest, FacebookLoginAppUserCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenHandler _tokenHandler;
        private readonly HttpClient _httpClient;

        public FacebookLoginAppUserCommandHandler(
            UserManager<AppUser> userManager,
            IConfiguration configuration,
            ITokenHandler tokenHandler,
            IHttpClientFactory httpClientFactory)
        {
            _userManager = userManager;
            _configuration = configuration;
            _tokenHandler = tokenHandler;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<FacebookLoginAppUserCommandResponse> Handle(FacebookLoginAppUserCommandRequest request, CancellationToken cancellationToken)
        {
            var clientId = _configuration["OAuth:Facebook:ClientId"];
            var clientSecret = _configuration["OAuth:Facebook:ClientSecret"];

            if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
                throw new InvalidOperationException("Facebook OAuth configuration is not properly set.");

            // 1. Generate App Access Token (Server-to-Server)
            var generateAccessTokenEndpoint =
                $"https://graph.facebook.com/oauth/access_token?client_id={clientId}&client_secret={clientSecret}&grant_type=client_credentials";

            var accessTokenResponse = await _httpClient.GetStringAsync(generateAccessTokenEndpoint, cancellationToken);
            if (string.IsNullOrEmpty(accessTokenResponse))
                throw new Exception("Failed to generate Facebook access token: Empty response.");

            var facebookToken = JsonSerializer.Deserialize<FacebookTokenResponseDTO>(accessTokenResponse);
            if (facebookToken is null || string.IsNullOrEmpty(facebookToken.AccessToken))
                throw new Exception("Failed to deserialize Facebook access token.");

            // 2. Verify the provided user token
            var verifyTokenEndpoint =
                $"https://graph.facebook.com/debug_token?input_token={request.AuthToken}&access_token={facebookToken.AccessToken}";
            var verifyTokenResponse = await _httpClient.GetStringAsync(verifyTokenEndpoint, cancellationToken);
            if (string.IsNullOrEmpty(verifyTokenResponse))
                throw new Exception("Failed to verify Facebook access token: Empty response.");

            var facebookValidateTokenResponse = JsonSerializer.Deserialize<FacebookTokenValidationResponseDTO>(verifyTokenResponse);
            if (facebookValidateTokenResponse?.Data == null || !facebookValidateTokenResponse.Data.IsValid)
                throw new Exception("Facebook access token is invalid or expired.");

            // 3. Retrieve user info from Facebook
            var userInfoEndpoint =
                $"https://graph.facebook.com/me?fields=id,email,first_name,last_name,picture&access_token={request.AuthToken}";
            var facebookUserInfoResponse = await _httpClient.GetStringAsync(userInfoEndpoint, cancellationToken);
            if (string.IsNullOrEmpty(facebookUserInfoResponse))
                throw new Exception("Failed to retrieve user info from Facebook: Empty response.");

            var facebookUserInfo = JsonSerializer.Deserialize<FacebookUserInfoResponseDTO>(facebookUserInfoResponse);
            if (facebookUserInfo == null || string.IsNullOrEmpty(facebookUserInfo.Id))
                throw new Exception("Failed to deserialize Facebook user info.");

            // 4. Find or create the user in our system
            // We'll attempt to find the user by external login first
            var loginInfo = new UserLoginInfo(request.Provider, facebookUserInfo.Id, request.Provider);
            var user = await _userManager.FindByLoginAsync(request.Provider, facebookUserInfo.Id);

            if (user == null)
            {
                // If not found by login, try to find by email (if email is available)
                if (!string.IsNullOrEmpty(facebookUserInfo.Email))
                {
                    user = await _userManager.FindByEmailAsync(facebookUserInfo.Email);
                }

                // If user still not found, create a new one
                if (user == null)
                {
                    user = new AppUser
                    {
                        UserName = facebookUserInfo.Email ?? $"{request.Provider}_{facebookUserInfo.Id}",
                        Email = facebookUserInfo.Email,
                        FullName = facebookUserInfo.FullName ?? $"{request.FirstName} {request.LastName}",
                    };

                    var createResult = await _userManager.CreateAsync(user);
                    if (!createResult.Succeeded)
                    {
                        var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                        throw new Exception($"Failed to create user: {errors}");
                    }
                }

                // Add the external login info if it doesn't exist
                var addLoginResult = await _userManager.AddLoginAsync(user, loginInfo);
                if (!addLoginResult.Succeeded)
                {
                    var errors = string.Join(", ", addLoginResult.Errors.Select(e => e.Description));
                    throw new Exception($"Failed to add external login info: {errors}");
                }
            }

            // 5. Generate our own token (e.g. JWT) for the user
            var token = _tokenHandler.GenerateToken(user.Id);

            // 6. Return the response
            return new FacebookLoginAppUserCommandResponse(token);
        }
    }
}
