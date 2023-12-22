using FluentValidation;

namespace BiteRight.Application.Commands.Users.Onboard;

public class OnboardValidator : AbstractValidator<OnboardRequest>
{
    public OnboardValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty();
    }
}