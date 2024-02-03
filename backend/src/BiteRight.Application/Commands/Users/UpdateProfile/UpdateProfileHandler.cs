using BiteRight.Application.Common;
using BiteRight.Application.Common.Exceptions;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Commands.Users.UpdateProfile;

public class UpdateProfileHandler : HandlerBase<UpdateProfileRequest>
{
    private readonly ILanguageRepository _languageRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IStringLocalizer<Resources.Resources.Users.Users> _localizer;
    private readonly IUserRepository _userRepository;
    private readonly IIdentityProvider _identityProvider;
    private readonly IDomainEventFactory _domainEventFactory;
    private readonly AppDbContext _appDbContext;

    public UpdateProfileHandler(
        ILanguageRepository languageRepository,
        ICurrencyRepository currencyRepository,
        ICountryRepository countryRepository,
        IStringLocalizer<Resources.Resources.Users.Users> localizer,
        IUserRepository userRepository,
        IIdentityProvider identityProvider,
        IDomainEventFactory domainEventFactory,
        AppDbContext appDbContext
    )
    {
        _languageRepository = languageRepository;
        _currencyRepository = currencyRepository;
        _countryRepository = countryRepository;
        _localizer = localizer;
        _userRepository = userRepository;
        _identityProvider = identityProvider;
        _domainEventFactory = domainEventFactory;
        _appDbContext = appDbContext;
    }

    protected override async Task<Unit> HandleImpl(
        UpdateProfileRequest request,
        CancellationToken cancellationToken
    )
    {
        var languageExists = await _languageRepository.ExistsById(request.LanguageId, cancellationToken);
        if (!languageExists)
        {
            throw ValidationException(_localizer[nameof(Resources.Resources.Users.Users.language_not_found)]);
        }

        var currencyExists = await _currencyRepository.ExistsById(request.CurrencyId, cancellationToken);
        if (!currencyExists)
        {
            throw ValidationException(_localizer[nameof(Resources.Resources.Users.Users.currency_not_found)]);
        }

        var countryExists = await _countryRepository.ExistsById(request.CountryId, cancellationToken);
        if (!countryExists)
        {
            throw ValidationException(_localizer[nameof(Resources.Resources.Users.Users.country_not_found)]);
        }
        
        var user = await _userRepository.FindByIdentityId(_identityProvider.RequireCurrent(), cancellationToken);
        
        if (user is null)
        {
            throw new InternalErrorException();
        }
        
        user.UpdateProfile(
            request.CountryId,
            request.LanguageId,
            request.CurrencyId,
            _domainEventFactory
        );
        
        return Unit.Value;
    }
}