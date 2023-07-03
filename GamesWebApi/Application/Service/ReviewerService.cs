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
    private readonly TokenGenerator _tokenGenerator;

    public ReviewerService(UserManager<Reviewer> userManager, TokenGenerator tokenGenerator)
    {
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ReviewerTokenDto?> CreateAccount(Reviewer reviewer, string password)
    {
        var result = await _userManager.CreateAsync(reviewer, password);

        if (!result.Succeeded)
        {
            throw new CreateAccountFailureException(result.Errors.Select(x => x.Description).ToArray());
        }

        var reviewerSaved = await _userManager.FindByEmailAsync(reviewer.Email);

        return _tokenGenerator.GenerateToken(reviewerSaved.Id, reviewerSaved.UserName);
    }
}