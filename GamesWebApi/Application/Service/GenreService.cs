using Application.Exception;
using Application.Service.Contracts;
using Domain.Entity;
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
        var genreFromDb = await _unitOfWork.GenreRepository.GetByNameAsync(genre.Name);

        if (genreFromDb != null)
        {
            throw new GenreAlreadyExistsException(genre.Name);
        }

        _unitOfWork.GenreRepository.CreateGenre(genre);
        await _unitOfWork.Commit();

        var genreSaved = await _unitOfWork.GenreRepository.GetByNameAsync(genre.Name);

        if (genreSaved is null)
        {
            throw new InternalServerErrorException();
        }

        return genreSaved;
    }

    public async Task<ICollection<Genre>> GetAll()
    {
        return await _unitOfWork.GenreRepository.GetAllAsync();
    }

    public async Task DeleteByName(string name)
    {
        var genre = await _unitOfWork.GenreRepository.GetByNameAsync(name);

        if (genre is null)
        {
            throw new GenreNotFoundException(name);
        }
        
        _unitOfWork.GenreRepository.Delete(genre);
        await _unitOfWork.Commit();
    }
}