using Domain.Repository;

namespace Infrastructure.Data;

public interface IUnitOfWork
{
    Task Commit();
    IGenreRepository GenreRepository { get; }
}