using FluentValidation;
using GamesWebApi.DTO;

namespace GamesWebApi.Validator;

public class CreateReviewValidator : AbstractValidator<CreateReviewDto>
{
    public CreateReviewValidator()
    {
        RuleFor(x => x.Description).MinimumLength(10).MaximumLength(1000)
            .WithMessage("Description length must be between 10 and 1000 chracters.");
        RuleFor(x => x.Stars).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5)
            .WithMessage("Stars must be between 1 and 5 stars.");
    }
}