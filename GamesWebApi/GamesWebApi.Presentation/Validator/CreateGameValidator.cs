using System.Net.Mime;
using FluentValidation;
using GamesWebApi.DTO;
using GamesWebApi.Helpers;

namespace GamesWebApi.Validator;

public class CreateGameValidator : AbstractValidator<CreateGameDto>
{
    public CreateGameValidator()
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
        RuleFor(x => x.Image.Length).Must(HaveLengthLessThanOrEqualFiveMb).WithMessage("Image too big.");
        RuleFor(x => x.Image.ContentType).Must(IsImageMediaTypeSupported).WithMessage("Image type not supported.");
        RuleFor(x => x.AgeRatingId).NotEmpty().WithMessage("Age rating must be provided.");
        RuleFor(x => x.GenresNames).Must(NotHaveDuplicates).WithMessage("Genres provided must not have duplicates.");
        RuleFor(x => x.PlatformsNames).Must(NotHaveDuplicates).WithMessage("Platforms provided must not have duplicates.");
    }

    private bool BeAValidDate(string date) => date.IsAValidDate();

    private bool IsGreaterThanZeroAndLessThanFive<T>(ICollection<T> list) => list.IsGreaterThanXAndLessThanY(0, 5);

    private bool HaveLengthLessThanOrEqualFiveMb(long imageSize) => imageSize <= 5 * 1024 * 1024;

    private bool IsImageMediaTypeSupported(string mediaType) =>
        mediaType is MediaTypeNames.Image.Jpeg or "image/jpg" or "image/png";

    private bool NotHaveDuplicates(ICollection<string> list) => list.NotHasDuplicates();
}