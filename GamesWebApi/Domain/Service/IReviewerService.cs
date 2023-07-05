using Domain.DTO;
using Domain.Entity;
using Domain.ValueObjects;

namespace Domain.Service;

public interface IReviewerService
{
    Task<ReviewerTokenDto?> CreateAccountAsync(Reviewer reviewer, string password);
    Task<ReviewerTokenDto> LoginAsync(string reviewerUserName, string password);
    Task DeleteAccountAsync(string username);
    Task<ReviewerInfo> GetReviewerInfoAsync(string username);
    Task<ICollection<Game>> GetGamesByUsernameAsync(string username);
    Task ChangePasswordAsync(string username, string oldPassword, string newPassword);
}