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

        var ageFromDb = await _unitOfWork.AgeRatingRepository.GetByIdAsync(game.AgeRatingId);
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

    public async Task<Game> GetGameByIdAsync(string gameId)
    {
        var gameExists = await _unitOfWork.GameRepository.GameExistsAsync(gameId);

        if (!gameExists)
        {
            throw new GameNotFoundException();
        }

        return await _unitOfWork.GameRepository.GetGameByIdAsync(gameId);
    }

    public async Task<Game> UpdateGameByIdAsync(Game game)
    {
        var gameExists = await _unitOfWork.GameRepository.GameExistsAsync(game.Id);

        if (!gameExists)
        {
            throw new GameNotFoundException();
        }

        var gameFromDb = await _unitOfWork.GameRepository.GetGameByIdAsync(game.Id);
        gameFromDb.Name = game.Name;
        gameFromDb.Description = game.Description;
        gameFromDb.Price = game.Price;
        gameFromDb.ReleaseDate = game.ReleaseDate;

        var ageExists = await _unitOfWork.AgeRatingRepository.AgeExistsAsync(game.AgeRatingId);

        if (!ageExists)
        {
            throw new AgeNotFoundException();
        }

        var age = await _unitOfWork.AgeRatingRepository.GetByIdAsync(game.AgeRatingId);
        gameFromDb.AgeRatingId = age.Id;
        gameFromDb.AgeRating = age;
        
        gameFromDb.Genres.Clear();
        foreach (var genre in game.Genres)
        {
            var genreFromDb = await _unitOfWork.GenreRepository.GetByNameAsync(genre.Name);

            if (genreFromDb is null)
            {
                throw new GenreNotFoundException(genre.Name);
            }
            
            gameFromDb.AddGenre(genreFromDb);
        }
        
        gameFromDb.Platforms.Clear();
        foreach (var platform in game.Platforms)
        {
            var platformFromDb = await _unitOfWork.PlatformRepository.GetByNameAsync(platform.Name);

            if (platformFromDb is null)
            {
                throw new PlatformNotFoundException(platform.Name);
            }
            
            gameFromDb.AddPlatform(platformFromDb);
        }

        await _unitOfWork.CommitAsync();

        var gameUpdated = await _unitOfWork.GameRepository.GetGameByIdAsync(game.Id);

        return gameUpdated;
    }
}