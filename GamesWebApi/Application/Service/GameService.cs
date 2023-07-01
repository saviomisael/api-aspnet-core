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
    
    public async Task<Game> CreateGame(Game game)
    {
        var age = await _unitOfWork.AgeRatingRepository.AgeExistsAsync(game.AgeRating.Id);

        if (!age)
        {
            throw new AgeNotFoundException();
        }
        
        foreach (var genre in game.Genres)
        {
            var exists = await _unitOfWork.GenreRepository.GenreExistsAsync(genre.Id);

            if (!exists)
            {
                throw new GenreNotFoundException();
            }
        }

        foreach (var platform in game.Platforms)
        {
            var exists = await _unitOfWork.PlatformRepository.PlatformExistsAsync(platform.Id);

            if (!exists)
            {
                throw new PlatformNotFoundException();
            }
        }
        
        _unitOfWork.GameRepository.SaveGame(game);
        await _unitOfWork.Commit();

        return await _unitOfWork.GameRepository.GetGameByIdAsync(game.Id);
    }
}