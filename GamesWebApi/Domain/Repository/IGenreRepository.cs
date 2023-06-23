using Domain.Entity;

namespace Domain.Repository;

public interface IGenreRepository
{
    Genre getByName(string name);
}