using Domain.Entity;

namespace Domain.Service;

public interface IGameService
{
    Task<Game> CreateGameAsync(Game game);
    Task<Game> GetGameByIdAsync(string gameId);
    Task<Game> UpdateGameByIdAsync(Game game);
    Task UpdateImageAsync(string urlImage, string gameId);
    Task DeleteGameByIdAsync(string gameId);
}