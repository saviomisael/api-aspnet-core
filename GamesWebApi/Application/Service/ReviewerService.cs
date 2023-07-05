using Application.Exception;
using Domain.DTO;
using Domain.Entity;
using Domain.Service;
using Domain.ValueObjects;
using Infrastructure.Jwt;
using Microsoft.AspNetCore.Identity;

namespace Application.Service;

public class ReviewerService : IReviewerService
{
    private readonly UserManager<Reviewer> _userManager;
    private readonly SignInManager<Reviewer> _signInManager;
    private readonly TokenGenerator _tokenGenerator;
    private readonly ISendChangePasswordNotificationService _changePasswordNotificationService;

    public ReviewerService(UserManager<Reviewer> userManager, TokenGenerator tokenGenerator,
        SignInManager<Reviewer> signInManager,
        ISendChangePasswordNotificationService changePasswordNotificationService) =>
        (_userManager, _tokenGenerator, _signInManager, _changePasswordNotificationService) = (userManager,
            tokenGenerator, signInManager, changePasswordNotificationService);

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
}