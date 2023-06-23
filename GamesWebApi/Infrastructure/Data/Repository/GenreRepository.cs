using Domain.Entity;
using Domain.Repository;

namespace Infrastructure.Data.Repository;

public class GenreRepository : IGenreRepository
{
    private readonly AppDbContext _context;

    public GenreRepository(AppDbContext context)
    {
        _context = context;
    }

    public Genre? getByName(string name)
    {
        return _context.Genres.FirstOrDefault(g => g.Name == name);
    }
}