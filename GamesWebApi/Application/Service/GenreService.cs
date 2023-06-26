using Application.Exception;
using Application.Service.Contracts;
using Domain.Entity;
using Domain.Repository;
using Infrastructure.Data;

namespace Application.Service;

public class GenreService : IGenreService
{
    private readonly IUnitOfWork _unitOfWork;
    public GenreService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Genre> CreateGenre(Genre genre)
    {
        var genreFromDb = await _unitOfWork.GenreRepository.GetByName(genre.Name);

        if (genreFromDb != null)
        {
            throw new GenreAlreadyExistsException(genre.Name);
        }

        _unitOfWork.GenreRepository.CreateGenre(genre);
        await _unitOfWork.Commit();

        var genreSaved = await _unitOfWork.GenreRepository.GetByName(genre.Name);

        if (genreSaved is null)
        {
            throw new InternalServerErrorException();
        }

        return genreSaved;
    }

    public ICollection<Genre> GetAll()
    {
        return _unitOfWork.GenreRepository.GetAll().ToList();
    }
}