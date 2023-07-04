using Domain.Entity;

namespace Domain.Service;

public interface IGameService
{
    Task<Game> CreateGameAsync(Game game);
    Task<Game> GetGameByIdAsync(string gameId);
    Task<Game> UpdateGameByIdAsync(Game game);
    Task UpdateImageAsync(string urlImage, string gameId);
    Task DeleteGameByIdAsync(string gameId);
    Task<Game> AddReviewAsync(string description, int stars, string gameId, string reviewerId);
    Task<Game> UpdateReviewAsync(Review review);
    Task<ICollection<Game>> GetAllAsync(int page = 1, bool descending = true, string sortType = "releaseDate", string term = "");
}