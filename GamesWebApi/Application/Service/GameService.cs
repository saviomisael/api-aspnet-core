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
        var age = await _unitOfWork.AgeRatingRepository.AgeExistsAsync(game.AgeRating.Id);
        var genresFromDb = new List<Genre>();

        if (!age)
        {
            throw new AgeNotFoundException();
        }

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
            var exists = await _unitOfWork.PlatformRepository.PlatformExistsAsync(platform.Id);

            if (!exists)
            {
                throw new PlatformNotFoundException();
            }
        }

        _unitOfWork.GameRepository.SaveGame(game);
        await _unitOfWork.CommitAsync();

        return await _unitOfWork.GameRepository.GetGameByIdAsync(game.Id);
    }
}