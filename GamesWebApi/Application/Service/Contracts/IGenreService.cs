using Domain.Entity;

namespace Application.Service.Contracts;

public interface IGenreService
{
    Task<Genre> CreateGenreAsync(Genre genre);
    Task<ICollection<Genre>> GetAllAsync();
    Task DeleteByNameAsync(string name);
}