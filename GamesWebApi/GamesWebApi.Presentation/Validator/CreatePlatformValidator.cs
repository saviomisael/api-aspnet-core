using FluentValidation;
using GamesWebApi.DTO;

namespace GamesWebApi.Validator;

public class CreatePlatformValidator : AbstractValidator<CreatePlatformDto>
{
    public CreatePlatformValidator()
    {
        RuleFor(x => x.Name).Length(3, 255).WithMessage("Platform must have a name with at least 3 characters and a maximum of 255 characters.");
    }
}