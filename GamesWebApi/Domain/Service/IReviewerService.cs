using Domain.DTO;
using Domain.Entity;

namespace Domain.Service;

public interface IReviewerService
{
    Task<ReviewerTokenDto?> CreateAccountAsync(Reviewer reviewer, string password);
    Task<ReviewerTokenDto> LoginAsync(string reviewerUserName, string password);
}