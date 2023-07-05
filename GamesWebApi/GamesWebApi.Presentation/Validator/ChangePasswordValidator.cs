using FluentValidation;
using GamesWebApi.DTO;

namespace GamesWebApi.Validator;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.NewPassword).NotEqual(x => x.CurrentPassword)
            .WithMessage("The new password must be different from the current password.");
        RuleFor(x => x.NewPassword).Equal(x => x.ConfirmPassword)
            .WithMessage("The new password and confirm password must be the equal.");
    }
}