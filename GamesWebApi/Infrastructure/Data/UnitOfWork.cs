using Domain.Repository;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IGenreRepository GenreRepository { get; }
    public IPlatformRepository PlatformRepository { get; }
    public IAgeRatingRepository AgeRatingRepository { get; }
    public IGameRepository GameRepository { get; }

    public UnitOfWork(AppDbContext context, IGenreRepository genreRepository, IPlatformRepository platformRepository, IAgeRatingRepository ageRatingRepository, IGameRepository gameRepository)
    {
        _context = context;
        GenreRepository = genreRepository;
        PlatformRepository = platformRepository;
        AgeRatingRepository = ageRatingRepository;
        GameRepository = gameRepository;
    }
    
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}