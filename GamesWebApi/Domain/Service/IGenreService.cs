using Domain.Entity;

namespace Domain.Service;

public interface IGenreService
{
    Task<Genre> CreateGenreAsync(Genre genre);
    Task<ICollection<Genre>> GetAllAsync();
    Task DeleteByNameAsync(string name);
}