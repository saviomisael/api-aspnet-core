using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository;

public class GenreRepository : IGenreRepository
{
    private readonly AppDbContext _context;

    public GenreRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Genre?> GetByNameAsync(string name)
    {
        return await _context.Genres.FirstOrDefaultAsync(g => g.Name == name);
    }

    public void CreateGenre(Genre genre)
    {
        _context.Genres.Add(genre);
    }

    public async Task<ICollection<Genre>> GetAllAsync()
    {
        return await _context.Genres.ToListAsync();
    }

    public void Delete(Genre genre)
    {
        _context.Genres.Remove(genre);
    }
}