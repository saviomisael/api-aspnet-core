using Domain.DTO;
using Domain.Entity;

namespace Domain.Service;

public interface IReviewerService
{
    Task<ReviewerTokenDto?> CreateAccount(Reviewer reviewer, string password);
}