using FluentValidation;
using GamesWebApi.DTO;

namespace GamesWebApi.Validator;

public class CreateGenreValidator : AbstractValidator<CreateGenreDto>
{
    public CreateGenreValidator()
    {
        RuleFor(x => x.Name).Length(3, 255).WithMessage("Genre must have a name with at least 3 characters and a maximum of 255 characters.");
    }
}