// # ==============================================================================
// # Solution: BiteRight
// # File: OnboardValidator.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using FluentValidation;
using Microsoft.Extensions.Localization;

#endregion

namespace BiteRight.Application.Commands.Users.Onboard;

public class OnboardValidator : AbstractValidator<OnboardRequest>
{
    public OnboardValidator(
        IStringLocalizer<Resources.Resources.Users.Users> usersLocalizer,
        IStringLocalizer<Resources.Resources.Currencies.Currencies> currenciesLocalizer
    )
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage(_ => usersLocalizer[nameof(Resources.Resources.Users.Users.username_empty)]);

        RuleFor(x => x.CurrencyId)
            .NotEmpty()
            .WithMessage(_ => currenciesLocalizer[nameof(Resources.Resources.Currencies.Currencies.currency_id_empty)]);

        RuleFor(x => x.TimeZoneId)
            .NotEmpty()
            .WithMessage(_ => usersLocalizer[nameof(Resources.Resources.Users.Users.time_zone_empty)]);
    }
}