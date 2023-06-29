using Domain.Entity;

namespace Domain.Service;

public interface IAgeRatingService
{
    Task<ICollection<AgeRating>> GetAllAsync();
}