using Domain.Entity;

namespace Domain.Service;

public interface IGameService
{
    Task<Game> CreateGameAsync(Game game);
    Task<Game> GetGameById(string gameId);
}