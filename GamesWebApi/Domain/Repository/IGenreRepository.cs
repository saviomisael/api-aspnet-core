using Domain.Entity;

namespace Domain.Repository;

public interface IGenreRepository
{
    Task<Genre?> GetByName(string name);
    void CreateGenre(Genre genre);
    Task<ICollection<Genre>> GetAll();
    void Delete(Genre genre);
}