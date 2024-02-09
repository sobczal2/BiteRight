using BiteRight.Application.Common;
using BiteRight.Application.Common.Exceptions;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Commands.Users.UpdateProfile;

public class UpdateProfileHandler : CommandHandlerBase<UpdateProfileRequest>
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IStringLocalizer<Resources.Resources.Currencies.Currencies> _currenciesLocalizer;
    private readonly IStringLocalizer<Resources.Resources.Users.Users> _usersLocalizer;
    private readonly IUserRepository _userRepository;
    private readonly IIdentityProvider _identityProvider;

    public UpdateProfileHandler(
        ICurrencyRepository currencyRepository,
        IStringLocalizer<Resources.Resources.Currencies.Currencies> currenciesLocalizer,
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
        {
            throw ValidationException(
                _currenciesLocalizer[nameof(Resources.Resources.Currencies.Currencies.currency_not_found)]);
        }

        var user = await _userRepository.FindByIdentityId(_identityProvider.RequireCurrent(), cancellationToken);

        if (user is null)
        {
            throw new InternalErrorException();
        }

        if (!TimeZoneInfo.TryFindSystemTimeZoneById(request.TimeZoneId, out var timeZone))
        {
            throw ValidationException(
                _usersLocalizer[nameof(Resources.Resources.Users.Users.time_zone_id_not_found)]);
        }

        user.UpdateProfile(
            request.CurrencyId,
            timeZone
        );

        return Unit.Value;
    }
}