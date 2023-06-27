using Domain.Entity;

namespace Application.Service.Contracts;

public interface IGenreService
{
    Task<Genre> CreateGenre(Genre genre);
    Task<ICollection<Genre>> GetAll();
    Task DeleteByName(string name);
}