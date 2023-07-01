using Domain.Repository;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IGenreRepository GenreRepository { get; set; } = null!;
    public IPlatformRepository PlatformRepository { get; set; } = null!;
    public IAgeRatingRepository AgeRatingRepository { get; set; } = null!;
    public IGameRepository GameRepository { get; set; } = null!;

    public UnitOfWork(AppDbContext context, IGenreRepository genreRepository, IPlatformRepository platformRepository, IAgeRatingRepository ageRatingRepository, IGameRepository gameRepository)
    {
        _context = context;
        GenreRepository = genreRepository;
        PlatformRepository = platformRepository;
        AgeRatingRepository = ageRatingRepository;
        GameRepository = gameRepository;
    }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}