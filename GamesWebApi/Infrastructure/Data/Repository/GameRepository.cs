using Domain.Entity;
using Domain.Repository;

namespace Infrastructure.Data.Repository;

public class GameRepository : IGameRepository
{
    private readonly AppDbContext _context;

    public GameRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public void SaveGame(Game game)
    {
        _context.Games.Add(game);
    }
}