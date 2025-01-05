using MediatR;

namespace CommercePortal.Application.Features.Commands.AppUsers.RegisterAppUser;

/// <summary>
/// Represents a handler for the <see cref="RegisterAppUserCommandRequest"/>
/// </summary>
public class RegisterAppUserCommandHandler : IRequestHandler<RegisterAppUserCommandRequest, RegisterAppUserCommandResponse>
{
    //public readonly UserManager<DomainUser> _userManager;

    //public RegisterAppUserCommandHandler(UserManager<DomainUser> userManager)
    //{
    //    _userManager = userManager;
    //}

    public async Task<RegisterAppUserCommandResponse> Handle(RegisterAppUserCommandRequest request, CancellationToken cancellationToken)
    {
        //var user = new AppUser
        //{
        //    FullName = request.FullName,
        //    Email = request.Email,
        //    UserName = request.UserName,
        //    PhoneNumber = request.PhoneNumber
        //};

        //var result = await _userManager.CreateAsync(user, request.Password);

        //if (result.Succeeded)
        //{
        //    return new RegisterAppUserCommandResponse(user.Id);
        //}

        //if (result.Errors.Any())
        //{
        //    var errors = result.Errors.Select(e => e.Description);
        //    throw new Exception($"Failed to create user: {string.Join(", ", errors)}");
        //}
        //else
        //{
        //    throw new Exception("Failed to create user");
        //}

        throw new NotImplementedException();
    }
}