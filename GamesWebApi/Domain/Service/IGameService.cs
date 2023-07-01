using Domain.Entity;

namespace Domain.Service;

public interface IGameService
{
    Task<Game> CreateGame(Game game);
}