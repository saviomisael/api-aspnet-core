using Domain.Entity;
using Domain.Repository;
using Domain.Service;

namespace Application.Service;

public class AgeRatingService : IAgeRatingService
{
    private readonly IAgeRatingRepository _repository;

    public AgeRatingService(IAgeRatingRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ICollection<AgeRating>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
}