using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository;

public class AgeRatingRepository : IAgeRatingRepository
{
    private readonly AppDbContext _context;

    public AgeRatingRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<AgeRating>> GetAllAsync()
    {
        return await _context.AgeRatings.ToListAsync();
    }

    public async Task<bool> AgeExistsAsync(string ageId)
    {
        var age = await _context.AgeRatings.FirstOrDefaultAsync(x => x.Id == ageId);

        return age != null;
    }

    public async Task<AgeRating> GetById(string id)
    {
        return await _context.AgeRatings.FirstAsync(x => x.Id == id);
    }
}