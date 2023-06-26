using Domain.Entity;

namespace Domain.Repository;

public interface IGenreRepository
{
    Task<Genre?> GetByName(string name);
    void CreateGenre(Genre genre);
    IQueryable<Genre> GetAll();
}