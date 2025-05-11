using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Application.Dtos.Identity;
using CleanArchitectureTemplate.Application.Exceptions;
using CleanArchitectureTemplate.Application.Features.Users.Commands.RegisterUser;
using CleanArchitectureTemplate.Application.Features.Users.Commands.UpdateUser;
using CleanArchitectureTemplate.Application.RequestParameters;
using CleanArchitectureTemplate.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CleanArchitectureTemplate.Persistence.Services.User.Identity;

/// <summary>
/// Represents a user service that is on top of the ASP.NET Core Identity mechanism.
/// </summary>
public class IdentityUserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    private readonly UserManager<AppUser> _userManager;

    public IdentityUserService(IConfiguration configuration, IEmailService emailService, UserManager<AppUser> userManager)
    {
        _configuration = configuration;
        _emailService = emailService;
        _userManager = userManager;
    }

    #region User Management

    /// <inheritdoc />
    public async Task<UserDto?> CreateAsync(RegisterUserCommandRequest model)
    {
        var user = model.ToAppUser();
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            return user.ToDto();
        }

        throw new BadRequestException(result.Errors.Select(e => e.Description).FirstOrDefault());
    }

    /// <inheritdoc />
    public async Task<UserDto?> UpdateAsync(UpdateUserCommandRequest model)
    {
        var user = await _userManager.FindByIdAsync(model.Id.ToString())
            ?? throw new NotFoundException(nameof(AppUser), model.Id);

        if (model.FullName != null)
            user.FullName = model.FullName;
        if (model.Email != null)
            user.Email = model.Email;
        if (model.UserName != null)
            user.UserName = model.UserName;
        if (model.PhoneNumber != null)
            user.PhoneNumber = model.PhoneNumber;

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return user.ToDto();
        }

        throw new BadRequestException(result.Errors.Select(e => e.Description).FirstOrDefault());
    }

    #endregion User Management

    #region User Retrieval

    /// <inheritdoc />
    public async Task<UserDto?> GetByIdAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString())
            ?? throw new NotFoundException(nameof(AppUser), id);

        return user.ToDto();
    }

    /// <inheritdoc />
    public async Task<(IEnumerable<UserDto> Data, int TotalCount)> GetAllPaginatedAsync(Pagination? pagination = null)
    {
        var query = _userManager.Users.AsNoTracking();

        var totalCount = await query.CountAsync();

        if (pagination != null)
        {
            var page = pagination.Page;
            var size = pagination.Size;

            query = query.Skip((page - 1) * size).Take(size);
        }

        var users = await query.ToListAsync();
        var userDtos = users.Select(u => u.ToDto());

        return (userDtos, totalCount);
    }

    #endregion User Retrieval

    #region Password Management

    /// <inheritdoc />
    public async Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString())
            ?? throw new NotFoundException(nameof(AppUser), userId);

        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        if (!result.Succeeded)
            throw new BadRequestException(result.Errors.Select(e => e.Description).FirstOrDefault());
    }

    /// <inheritdoc />
    public async Task ForgotPasswordAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email)
            ?? throw new NotFoundException("User with this email does not exist.");

        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        // Retrieve and compose the reset password URL
        string baseUrl = _configuration["FrontEnd:BaseUrl"]?.TrimEnd('/')!;
        string resetPasswordPath = _configuration["FrontEnd:ResetPasswordPath"]!;

        // Format the path by replacing placeholders
        string resetPasswordUrl = $"{baseUrl}{resetPasswordPath}"
            .Replace("{userId}", Uri.EscapeDataString(user.Id.ToString()))
            .Replace("{token}", Uri.EscapeDataString(resetToken));

        // Compose email content
        string subject = "Password Reset Request - Clean Architecture Template";
        string body = $@"
            <p>Hello <strong>{user.UserName}</strong>,</p>
            <p>You requested a password reset. Click the <a href='{resetPasswordUrl}' target='_blank'>here</a> to reset your password:</p>
            <p>If you did not request this, please ignore this email.</p>
            <p>Best regards,<br>Clean Architecture Template Support Team</p>";

        await _emailService.SendEmailAsync(
            toAddresses: [user.Email!],
            subject: subject,
            body: body,
            isHtml: true,
            ccAddresses: null,
            bccAddresses: null,
            attachments: null
        );
    }

    /// <inheritdoc />
    public async Task ResetPasswordAsync(Guid userId, string token, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString())
            ?? throw new NotFoundException(nameof(AppUser), userId);

        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

        if (!result.Succeeded)
            throw new BadRequestException(result.Errors.Select(e => e.Description).FirstOrDefault());
    }

    #endregion Password Management
}