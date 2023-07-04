using Domain.DTO;
using Domain.Entity;

namespace GamesWebApi.Mapper;

public static class ReviewerMapper
{
    public static ReviewerResponseDto FromEntityToReviewerResponseDto(Reviewer entity)
    {
        return new ReviewerResponseDto
        {
            Id = entity.Id,
            UserName = entity.UserName
        };
    }
}