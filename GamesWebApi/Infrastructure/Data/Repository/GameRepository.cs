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
}