using Domain.DTO;
using Domain.Entity;

namespace GamesWebApi.Mapper;

public static class ReviewMapper
{
    public static ReviewResponseDto FromEntityToReviewResponseDto(Review entity)
    {
        return new ReviewResponseDto
        {
            Id = entity.Id,
            Description = entity.Description,
            Stars = entity.Stars,
            Reviewer = ReviewerMapper.FromEntityToReviewerResponseDto(entity.Reviewer)
        };
    }
}