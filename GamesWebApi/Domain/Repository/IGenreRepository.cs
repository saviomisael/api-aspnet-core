using Domain.Entity;

namespace Domain.Repository;

public interface IGenreRepository
{
    Task<Genre?> GetByName(string name);
    Task CreateGenre(Genre genre);
}