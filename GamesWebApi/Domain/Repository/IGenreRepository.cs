using Domain.Entity;

namespace Domain.Repository;

public interface IGenreRepository
{
    Genre? GetByName(string name);
    Task CreateGenre(Genre genre);
}