using Domain.Entity;

namespace Domain.Repository;

public interface IAgeRatingRepository
{
    Task<ICollection<AgeRating>> GetAllAsync();
    Task<bool> AgeExistsAsync(string ageId);
}