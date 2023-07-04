using System.Collections;
using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository;

public class GameRepository : IGameRepository
{
    private readonly AppDbContext _context;
    private const int MaxGamesPerPage = 9;

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

    public async Task<bool> GameExistsAsync(string gameId)
    {
        var game = await _context.Games.FirstOrDefaultAsync(x => x.Id == gameId);

        return game != null;
    }

    public void DeleteGame(Game game)
    {
        _context.Games.Remove(game);
    }

    public void AddReview(Review review)
    {
        _context.Reviews.Add(review);
    }

    public async Task<Review> UpdateReviewAsync(Review review)
    {
        var reviewFromDb = await _context.Reviews.FirstAsync(x => x.Id == review.Id);

        reviewFromDb.Description = review.Description;
        reviewFromDb.Stars = review.Stars;

        return reviewFromDb;
    }

    public async Task<bool> ReviewExistsAsync(string reviewId)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
        return review != null;
    }

    public async Task<ICollection<Game>> GetAllAsync(int page, bool descending,
        string sortType, string term)
    {
        if (term.Length > 0)
        {
            term = $"\"{term}\"";

            if (!descending)
            {
                return await SearchGameOrderByReleaseDateInAscendingOrderAsync(page, term);
            }

            return await SearchGameOrderByReleaseDateInDescendingOrderAsync(page, term);
        }

        if (!descending)
        {
            return await GetGamesOrderByReleaseDateInAscendingOrderAsync(page);
        }

        return await GetGamesOrderByReleaseDateInDescendingOrderAsync(page);
    }

    public async Task<int> GetMaxPagesAsync()
    {
        var games = await _context.Games.ToListAsync();

        return (int)Math.Ceiling((double)games.Count / MaxGamesPerPage);
    }

    public async Task<int> GetMaxPagesBySearchAsync(string term)
    {
        term = $"\"{term}\"";
        
        var games = await _context.Games.Where(x =>
            EF.Functions.Contains(x.Name, term) || x.Genres.Any(y => EF.Functions.Contains(y.Name, term)) ||
            x.Platforms.Any(y => EF.Functions.Contains(y.Name, term))).ToListAsync();

        return (int)Math.Ceiling((double)games.Count / MaxGamesPerPage);
    }

    public async Task<bool> IsGameNameInUseAsync(string name)
    {
        var game = await _context.Games.FirstOrDefaultAsync(x => x.Name == name);

        return game != null;
    }

    private async Task<ICollection<Game>> GetGamesOrderByReleaseDateInDescendingOrderAsync(int page)
    {
        return await _context.Games.OrderByDescending(x => x.ReleaseDate)
            .Skip(page < 2 ? 0 : (page - 1) * MaxGamesPerPage).Take(MaxGamesPerPage).ToListAsync();
    }

    private async Task<ICollection<Game>> GetGamesOrderByReleaseDateInAscendingOrderAsync(int page)
    {
        return await _context.Games.OrderBy(x => x.ReleaseDate).Skip(page < 2 ? 0 : (page - 1) * MaxGamesPerPage)
            .Take(MaxGamesPerPage).ToListAsync();
    }

    private async Task<ICollection<Game>> SearchGameOrderByReleaseDateInDescendingOrderAsync(int page, string term)
    {
        return await _context.Games.Where(x =>
                EF.Functions.Contains(x.Name, term) || x.Genres.Any(y => EF.Functions.Contains(y.Name, term)) ||
                x.Platforms.Any(y => EF.Functions.Contains(y.Name, term)))
            .OrderByDescending(x => x.ReleaseDate).Skip(page < 2 ? 0 : (page - 1) * MaxGamesPerPage)
            .Take(MaxGamesPerPage).ToListAsync();
    }

    private async Task<ICollection<Game>> SearchGameOrderByReleaseDateInAscendingOrderAsync(int page, string term)
    {
        return await _context.Games.Where(x =>
                EF.Functions.Contains(x.Name, term) || x.Genres.Any(y => EF.Functions.Contains(y.Name, term)) ||
                x.Platforms.Any(y => EF.Functions.Contains(y.Name, term)))
            .OrderBy(x => x.ReleaseDate).Skip(page < 2 ? 0 : (page - 1) * MaxGamesPerPage).Take(MaxGamesPerPage)
            .ToListAsync();
    }
}