using FluentValidation;
using GamesWebApi.DTO;

namespace GamesWebApi.Validator;

public class CreateAccountValidator : AbstractValidator<CreateAccountDto>
{
    public CreateAccountValidator()
    {
        RuleFor(x => x.UserName).MinimumLength(3).MaximumLength(255)
            .WithMessage("UserName length must be between 3 and 255 characters.");
        RuleFor(x => x.Password).Equal(x => x.ConfirmPassword)
            .WithMessage("Password and ConfirmPassword must be the same.");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email format.");
    }
}