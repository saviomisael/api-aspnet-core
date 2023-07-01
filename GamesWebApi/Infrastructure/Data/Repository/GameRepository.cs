using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Game> GetGameByIdAsync(string gameId)
    {
        return await _context.Games.FirstAsync(x => x.Id == gameId);
    }
}