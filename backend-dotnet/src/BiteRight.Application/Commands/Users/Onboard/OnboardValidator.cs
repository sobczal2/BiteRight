using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Commands.Users.Onboard;

public class OnboardValidator : AbstractValidator<OnboardRequest>
{
    public OnboardValidator(
        IStringLocalizer<Resources.Resources.Onboard.Onboard> localizer
        )
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage(_ => localizer[nameof(Resources.Resources.Onboard.Onboard_en.username_empty)]);
    }
}