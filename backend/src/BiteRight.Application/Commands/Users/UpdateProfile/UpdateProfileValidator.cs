using BiteRight.Resources.Resources.Currencies;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Commands.Users.UpdateProfile;

public class UpdateProfileValidator : AbstractValidator<UpdateProfileRequest>
{
    public UpdateProfileValidator(
        IStringLocalizer<Resources.Resources.Users.Users> usersLocalizer
    )
    {
        RuleFor(x => x.CurrencyId)
            .NotEmpty()
            .WithMessage(usersLocalizer[nameof(Currencies.currency_id_empty)]);

        RuleFor(x => x.TimeZoneId)
            .NotEmpty()
            .WithMessage(usersLocalizer[nameof(Resources.Resources.Users.Users.time_zone_empty)]);
    }
}