using BiteRight.Application.Common;
using BiteRight.Application.Common.Exceptions;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Infrastructure.Database;
using BiteRight.Resources.Resources.Currencies;
using MediatR;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Commands.Users.UpdateProfile;

public class UpdateProfileHandler : CommandHandlerBase<UpdateProfileRequest>
{
    private readonly IStringLocalizer<Currencies> _currenciesLocalizer;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IIdentityProvider _identityProvider;
    private readonly IUserRepository _userRepository;
    private readonly IStringLocalizer<Resources.Resources.Users.Users> _usersLocalizer;

    public UpdateProfileHandler(
        ICurrencyRepository currencyRepository,
        IStringLocalizer<Currencies> currenciesLocalizer,
        IStringLocalizer<Resources.Resources.Users.Users> usersLocalizer,
        IUserRepository userRepository,
        IIdentityProvider identityProvider,
        AppDbContext appAppDbContext
    ) : base(appAppDbContext)
    {
        _currencyRepository = currencyRepository;
        _currenciesLocalizer = currenciesLocalizer;
        _usersLocalizer = usersLocalizer;
        _userRepository = userRepository;
        _identityProvider = identityProvider;
    }

    protected override async Task<Unit> HandleImpl(
        UpdateProfileRequest request,
        CancellationToken cancellationToken
    )
    {
        var currencyExists = await _currencyRepository.ExistsById(request.CurrencyId, cancellationToken);
        if (!currencyExists)
            throw ValidationException(
                _currenciesLocalizer[nameof(Currencies.currency_not_found)]);

        var user = await _userRepository.FindByIdentityId(_identityProvider.RequireCurrent(), cancellationToken);

        if (user is null) throw new InternalErrorException();

        if (!TimeZoneInfo.TryFindSystemTimeZoneById(request.TimeZoneId, out var timeZone))
            throw ValidationException(
                _usersLocalizer[nameof(Resources.Resources.Users.Users.time_zone_id_not_found)]);

        user.UpdateProfile(
            request.CurrencyId,
            timeZone
        );

        return Unit.Value;
    }
}