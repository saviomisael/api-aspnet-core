using Domain.Entity;

namespace Infrastructure.Data;

public class DataSeeder
{
    private readonly AppDbContext _context;

    public DataSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async void Seed()
    {
        if (_context.AgeRatings.Any()) return;

        var ageRatingsIarc = new List<AgeRating>()
        {
            new AgeRating("3+", "Content suitable for ages 3 and above only."),
            new AgeRating("7+", "Content suitable for ages 7 and above only."),
            new AgeRating("12+", "Content suitable for ages 12 and above only."),
            new AgeRating("16+", "Content suitable for ages 16 and above only."),
            new AgeRating("18+", "Content suitable for ages 18 and above only.")
        };

        await _context.AgeRatings.AddRangeAsync(ageRatingsIarc);
        await _context.SaveChangesAsync();
    }
}