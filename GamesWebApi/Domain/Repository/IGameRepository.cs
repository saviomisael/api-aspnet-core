using Domain.Entity;

namespace Domain.Repository;

public interface IGameRepository
{
    void SaveGame(Game game);
    Task<Game> GetGameByIdAsync(string gameId);
}