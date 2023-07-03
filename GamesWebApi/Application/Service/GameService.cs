using Application.Exception;
using Domain.Entity;
using Domain.Service;
using Infrastructure.Data;

namespace Application.Service;

public class GameService : IGameService
{
    private readonly IUnitOfWork _unitOfWork;

    public GameService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Game> CreateGameAsync(Game game)
    {
        var age = await _unitOfWork.AgeRatingRepository.AgeExistsAsync(game.AgeRatingId);
        if (!age)
        {
            throw new AgeNotFoundException();
        }

        var ageFromDb = await _unitOfWork.AgeRatingRepository.GetById(game.AgeRatingId);
        game.AgeRating = ageFromDb;
        
        var genresFromDb = new List<Genre>();
        var platformsFromDb = new List<Platform>();

        foreach (var genre in game.Genres)
        {
            var genreFromDb = await _unitOfWork.GenreRepository.GetByNameAsync(genre.Name);

            if (genreFromDb is null)
            {
                throw new GenreNotFoundException();
            }

            genresFromDb.Add(genreFromDb);
        }

        game.Genres.Clear();
        game.Genres = genresFromDb;

        foreach (var platform in game.Platforms)
        {
            var platformFromDb = await _unitOfWork.PlatformRepository.GetByNameAsync(platform.Name);

            if (platformFromDb is null)
            {
                throw new PlatformNotFoundException();
            }
            
            platformsFromDb.Add(platformFromDb);
        }
        game.Platforms.Clear();
        game.Platforms = platformsFromDb;

        _unitOfWork.GameRepository.SaveGame(game);
        await _unitOfWork.CommitAsync();

        return await _unitOfWork.GameRepository.GetGameByIdAsync(game.Id);
    }

    public async Task<Game> GetGameById(string gameId)
    {
        var gameExists = await _unitOfWork.GameRepository.GameExists(gameId);

        if (!gameExists)
        {
            throw new GameNotFoundException();
        }

        return await _unitOfWork.GameRepository.GetGameByIdAsync(gameId);
    }
}