using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Commands.Users.Onboard;

public class OnboardValidator : AbstractValidator<OnboardRequest>
{
    public OnboardValidator(
        IStringLocalizer<Resources.Resources.Users.Users> localizer
        )
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage(_ => localizer[nameof(Resources.Resources.Users.Users.username_empty)]);
    }
}