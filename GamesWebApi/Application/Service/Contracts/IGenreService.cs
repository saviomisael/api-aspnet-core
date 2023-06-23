using Domain.Entity;

namespace Application.Service.Contracts;

public interface IGenreService
{
    Task<Genre> CreateGenre(Genre genre);
}