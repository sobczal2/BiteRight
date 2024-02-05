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
    private readonly ILanguageRepository _languageRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IStringLocalizer<Resources.Resources.Users.Users> _usersLocalizer;
    private readonly IStringLocalizer<Resources.Resources.Currencies.Currencies> _currenciesLocalizer;
    private readonly IUserRepository _userRepository;
    private readonly IIdentityProvider _identityProvider;
    private readonly IDomainEventFactory _domainEventFactory;

    public UpdateProfileHandler(
        ILanguageRepository languageRepository,
        ICurrencyRepository currencyRepository,
        ICountryRepository countryRepository,
        IStringLocalizer<Resources.Resources.Users.Users> usersLocalizer,
        IStringLocalizer<Resources.Resources.Currencies.Currencies> currenciesLocalizer,
        IUserRepository userRepository,
        IIdentityProvider identityProvider,
        IDomainEventFactory domainEventFactory,
        AppDbContext appAppDbContext
    ) : base(appAppDbContext)
    {
        _languageRepository = languageRepository;
        _currencyRepository = currencyRepository;
        _countryRepository = countryRepository;
        _usersLocalizer = usersLocalizer;
        _currenciesLocalizer = currenciesLocalizer;
        _userRepository = userRepository;
        _identityProvider = identityProvider;
        _domainEventFactory = domainEventFactory;
    }

    protected override async Task<Unit> HandleImpl(
        UpdateProfileRequest request,
        CancellationToken cancellationToken
    )
    {
        var currencyExists = await _currencyRepository.ExistsById(request.CurrencyId, cancellationToken);
        if (!currencyExists)
        {
            throw ValidationException(_currenciesLocalizer[nameof(Resources.Resources.Currencies.Currencies.currency_not_found)]);
        }
        
        var user = await _userRepository.FindByIdentityId(_identityProvider.RequireCurrent(), cancellationToken);
        
        if (user is null)
        {
            throw new InternalErrorException();
        }
        
        user.UpdateProfile(
            request.CurrencyId,
            _domainEventFactory
        );
        
        return Unit.Value;
    }
}