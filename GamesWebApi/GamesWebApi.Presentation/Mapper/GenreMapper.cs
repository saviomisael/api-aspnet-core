using Domain.DTO;
using Domain.Entity;

namespace GamesWebApi.Mapper;

public static class GenreMapper
{
    public static GenreResponseDto FromEntityToGenreResponseDto(Genre genre)
    {
        return new GenreResponseDto
        {
            Id = genre.Id,
            Name = genre.Name
        };
    }
}