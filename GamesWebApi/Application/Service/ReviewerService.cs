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
        var result = await _userManager.CreateAsync(reviewer, password);

        if (!result.Succeeded)
        {
            throw new CreateAccountFailureException(result.Errors.Select(x => x.Description).ToArray());
        }

        var reviewerSaved = await _userManager.FindByEmailAsync(reviewer.Email);

        return _tokenGenerator.GenerateToken(reviewerSaved.Id, reviewerSaved.UserName);
    }

    public async Task<ReviewerTokenDto> LoginAsync(string reviewerEmail, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(reviewerEmail, password, false, false);

        if (!result.Succeeded)
        {
            throw new LoginFailureException();
        }

        var account = await _userManager.FindByEmailAsync(reviewerEmail);

        return _tokenGenerator.GenerateToken(account.Id, account.UserName);
    }
}