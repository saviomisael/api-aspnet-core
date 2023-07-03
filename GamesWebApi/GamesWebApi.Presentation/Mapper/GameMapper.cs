using Domain.DTO;
using Domain.Entity;
using GamesWebApi.DTO;

namespace GamesWebApi.Mapper;

public static class GameMapper
{
    public static Game FromCreateGameDtoToEntity(CreateGameDto dto, string imageUrl)
    {
        var game = new Game()
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            ReleaseDate = DateTime.Parse(dto.ReleaseDate),
            AgeRatingId = dto.AgeRatingId,
            UrlImage = imageUrl
        };

        foreach (var name in dto.GenresNames)
        {
            game.AddGenre(new Genre { Name = name});
        }

        foreach (var name in dto.PlatformsNames)
        {
            game.AddPlatform(new Platform { Name = name });
        }

        return game;
    }

    public static Game FromUpdateGameDtoToEntity(UpdateGameDto dto, string gameId)
    {
        var game = new Game
        {
            Id = gameId,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            ReleaseDate = DateTime.Parse(dto.ReleaseDate),
            AgeRatingId = dto.AgeRatingId
        };

        foreach (var name in dto.GenresNames)
        {
            game.AddGenre(new Genre(name));
        }

        foreach (var name in dto.PlatformsNames)
        {
            game.AddPlatform(new Platform(name));
        }

        return game;
    }

    public static GameResponseDto FromEntityToGameResponseDto(Game game)
    {
        return new GameResponseDto
        {
            Id = game.Id,
            Description = game.Description,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate,
            UrlImage = game.UrlImage,
            Name = game.Name,
            Genres = game.Genres.Select(GenreMapper.FromEntityToGenreResponseDto).ToList(),
            Platforms = game.Platforms.Select(PlatformMapper.FromEntityToPlatformResponseDto).ToList(),
            AgeRating = AgeRatingMapper.FromEntityToAgeRatingResponseDto(game.AgeRating)
        };
    }
}