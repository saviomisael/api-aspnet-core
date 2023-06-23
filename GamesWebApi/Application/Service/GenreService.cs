using Application.Exception;
using Application.Service.Contracts;
using Domain.Entity;
using Domain.Repository;

namespace Application.Service;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _repository;
    public GenreService(IGenreRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Genre> CreateGenre(Genre genre)
    {
        var genreFromDb = await _repository.GetByName(genre.Name);

        if (genreFromDb != null)
        {
            throw new GenreAlreadyExistsException(genre.Name);
        }

        await _repository.CreateGenre(genre);

        var genreSaved = await _repository.GetByName(genre.Name);

        if (genreSaved is null)
        {
            throw new InternalServerErrorException();
        }

        return genreSaved;
    }
}