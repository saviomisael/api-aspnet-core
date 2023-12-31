using Domain.Entity;

namespace Domain.Repository;

public interface IGameRepository
{
    void SaveGame(Game game);
    Task<Game> GetGameByIdAsync(string gameId);
    Task<bool> GameExistsAsync(string gameId);
    void DeleteGame(Game game);
    void AddReview(Review review);

    Task<Review> UpdateReviewAsync(Review review);
    Task<bool> ReviewExistsAsync(string reviewId);
    Task<ICollection<Game>> GetAllAsync(int page, bool descending, string sortType, string term);
    Task<int> GetMaxPagesAsync();
    Task<int> GetMaxPagesBySearchAsync(string term);
    Task<bool> IsGameNameInUseAsync(string name);
}