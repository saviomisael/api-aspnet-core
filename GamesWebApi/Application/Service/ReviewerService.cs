using Application.Exception;
using Domain.DTO;
using Domain.Entity;
using Domain.Service;
using Infrastructure.Jwt;
using Microsoft.AspNetCore.Identity;

namespace Application.Service;

public class ReviewerService : IReviewerService
{
    private readonly UserManager<Reviewer> _userManager;
    private readonly SignInManager<Reviewer> _signInManager;
    private readonly TokenGenerator _tokenGenerator;

    public ReviewerService(UserManager<Reviewer> userManager, TokenGenerator tokenGenerator, SignInManager<Reviewer> signInManager)
    {
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
        _signInManager = signInManager;
    }

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
}