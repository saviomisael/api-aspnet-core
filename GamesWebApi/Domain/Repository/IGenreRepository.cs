using Domain.Entity;

namespace Domain.Repository;

public interface IGenreRepository
{
    Task<Genre?> GetByNameAsync(string name);
    void CreateGenre(Genre genre);
    Task<ICollection<Genre>> GetAllAsync();
    void Delete(Genre genre);
}