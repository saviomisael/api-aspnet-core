using Domain.Repository;

namespace Infrastructure.Data;

public interface IUnitOfWork
{
    Task CommitAsync();
    IGenreRepository GenreRepository { get; }
    IPlatformRepository PlatformRepository { get; }
    IAgeRatingRepository AgeRatingRepository { get; }
    IGameRepository GameRepository { get; }
}