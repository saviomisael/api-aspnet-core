using Domain.Entity;

namespace Domain.Repository;

public interface IGameRepository
{
    void saveGame(Game game);
}