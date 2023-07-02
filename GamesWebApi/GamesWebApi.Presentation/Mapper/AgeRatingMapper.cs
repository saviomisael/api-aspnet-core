using Domain.DTO;
using Domain.Entity;

namespace GamesWebApi.Mapper;

public static class AgeRatingMapper
{
    public static AgeRatingResponseDto FromEntityToAgeRatingResponseDto(AgeRating ageRating)
    {
        return new AgeRatingResponseDto
        {
            Id = ageRating.Id,
            Age = ageRating.Age,
            Description = ageRating.Description
        };
    }
}