using FluentValidation;
using GamesWebApi.DTO;
using GamesWebApi.Helpers;

namespace GamesWebApi.Validator;

public class UpdateGameValidator : AbstractValidator<UpdateGameDto>
{
    public UpdateGameValidator()
    {
        RuleFor(x => x.Name).MaximumLength(255).MinimumLength(3)
            .WithMessage("Game name must have between 3 and 255 characters.");
        RuleFor(x => x.Description).MaximumLength(1000).MinimumLength(10)
            .WithMessage("Game description must have between 10 and 1000 characters.");
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Game price must be 0 or positive.");
        RuleFor(x => x.ReleaseDate).Must(BeAValidDate)
            .WithMessage("Release date must be a valid date and must be in the past.");
        RuleFor(x => x.GenresNames).Must(IsGreaterThanZeroAndLessThanFive)
            .WithMessage("Genres provided must have at least 1 genre and no more than 4 genres.");
        RuleFor(x => x.PlatformsNames).Must(IsGreaterThanZeroAndLessThanFive)
            .WithMessage("Platforms provided must have at least 1 platform and no more than 4 platforms.");
        RuleFor(x => x.AgeRatingId).NotEmpty().WithMessage("Age rating must be provided.");
        RuleFor(x => x.GenresNames).Must(NotHaveDuplicates).WithMessage("Genres provided must not have duplicates.");
        RuleFor(x => x.PlatformsNames).Must(NotHaveDuplicates).WithMessage("Platforms provided must not have duplicates.");
    }
    
    private bool NotHaveDuplicates(ICollection<string> list) => list.NotHasDuplicates();
    private bool IsGreaterThanZeroAndLessThanFive<T>(ICollection<T> list) => list.IsGreaterThanXAndLessThanY(0, 5);

    private bool BeAValidDate(string date) => date.IsAValidDate();
}