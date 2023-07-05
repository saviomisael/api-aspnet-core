using Application.Exception;
using Domain.DTO;
using Domain.Entity;
using Domain.Service;
using Domain.ValueObjects;
using Infrastructure;
using Infrastructure.Jwt;
using Microsoft.AspNetCore.Identity;

namespace Application.Service;

public class ReviewerService : IReviewerService
{
    private readonly UserManager<Reviewer> _userManager;
    private readonly SignInManager<Reviewer> _signInManager;
    private readonly TokenGenerator _tokenGenerator;
    private readonly ISendChangePasswordNotificationService _changePasswordNotificationService;
    private readonly ISendTemporaryPasswordNotificationService _temporaryPasswordNotificationService;

    public ReviewerService(UserManager<Reviewer> userManager, TokenGenerator tokenGenerator,
        SignInManager<Reviewer> signInManager,
        ISendChangePasswordNotificationService changePasswordNotificationService,
        ISendTemporaryPasswordNotificationService temporaryPasswordNotificationService) =>
    (
        _userManager,
        _tokenGenerator,
        _signInManager,
        _changePasswordNotificationService,
        _temporaryPasswordNotificationService
    ) = (
        userManager,
        tokenGenerator,
        signInManager,
        changePasswordNotificationService,
        temporaryPasswordNotificationService
    );

    public async Task<ReviewerTokenDto?> CreateAccountAsync(Reviewer reviewer, string password)
    {
        var emailIsInUse = await _userManager.FindByEmailAsync(reviewer.Email);

        if (emailIsInUse != null)
        {
            throw new EmailInUseException(reviewer.Email);
        }

        var result = await _userManager.CreateAsync(reviewer, password);

        if (!result.Succeeded)
        {
            throw new CreateAccountFailureException(result.Errors.Select(x => x.Description).ToArray());
        }

        var reviewerSaved = await _userManager.FindByEmailAsync(reviewer.Email);

        return _tokenGenerator.GenerateToken(reviewerSaved.Id, reviewerSaved.UserName);
    }

    public async Task<ReviewerTokenDto> LoginAsync(string reviewerUserName, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(reviewerUserName, password, false, false);

        if (!result.Succeeded)
        {
            throw new LoginFailureException();
        }

        var account = await _userManager.FindByNameAsync(reviewerUserName);

        return _tokenGenerator.GenerateToken(account.Id, account.UserName);
    }

    public async Task DeleteAccountAsync(string username)
    {
        var reviewer = await _userManager.FindByNameAsync(username);

        if (reviewer is null)
        {
            throw new ReviewerNotFoundException(username);
        }

        await _userManager.DeleteAsync(reviewer);
    }

    public async Task<ReviewerInfo> GetReviewerInfoAsync(string username)
    {
        var reviewer = await _userManager.FindByNameAsync(username);

        if (reviewer is null)
        {
            throw new ReviewerNotFoundException();
        }

        return new ReviewerInfo
        {
            ReviewsCount = reviewer.Reviews.Count,
            CreatedAtUtcTime = reviewer.CreatedAt
        };
    }

    public async Task<ICollection<Game>> GetGamesByUsernameAsync(string username)
    {
        var reviewer = await _userManager.FindByNameAsync(username);

        if (reviewer is null)
        {
            throw new ReviewerNotFoundException();
        }

        return reviewer.Reviews.Select(x => x.Game).ToList();
    }

    public async Task ChangePasswordAsync(string username, string oldPassword, string newPassword)
    {
        var reviewer = await _userManager.FindByNameAsync(username);

        if (reviewer is null)
        {
            throw new ReviewerNotFoundException();
        }

        var result = await _userManager.ChangePasswordAsync(reviewer, oldPassword, newPassword);

        if (!result.Succeeded)
        {
            throw new ChangePasswordFailureException(result.Errors.Select(x => x.Description).ToArray());
        }

        _changePasswordNotificationService.SendNotification(new EmailReceiverDto
        {
            Email = reviewer.Email,
            UserName = reviewer.UserName
        });
    }

    public async Task CreateTemporaryPasswordAsync(string email)
    {
        var reviewer = await _userManager.FindByEmailAsync(email);

        if (reviewer is null)
        {
            throw new ReviewerNotFoundException();
        }

        var randomPassword = RandomPassword.Generate();
        reviewer.TemporaryPassword = PasswordEncrypter.Encrypt(randomPassword);
        reviewer.TempPasswordTime = DateTime.UtcNow.AddHours(1);
        var result = await _userManager.UpdateAsync(reviewer);
        if (!result.Succeeded)
        {
            throw new InternalServerErrorException();
        }
        
        _temporaryPasswordNotificationService.SendNotification(new ForgotPasswordEmailReceiverDto
        {
            UserName = reviewer.UserName,
            Email = reviewer.Email,
            RandomPassword = randomPassword
        });
    }
}