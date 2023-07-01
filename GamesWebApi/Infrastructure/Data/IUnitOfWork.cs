using Domain.Repository;

namespace Infrastructure.Data;

public interface IUnitOfWork
{
    Task CommitAsync();
    IGenreRepository GenreRepository { get; set; }
    IPlatformRepository PlatformRepository { get; set; }
    IAgeRatingRepository AgeRatingRepository { get; set; }
    IGameRepository GameRepository { get; set; }
}