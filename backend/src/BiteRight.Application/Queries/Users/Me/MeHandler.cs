using BiteRight.Application.Common.Exceptions;
using BiteRight.Application.Dtos.Users;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;
using MediatR;

namespace BiteRight.Application.Queries.Users.Me;

public class MeHandler : IRequestHandler<MeRequest, MeResponse>
{
    private readonly IIdentityProvider _identityProvider;
    private readonly IUserRepository _userRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ILanguageRepository _languageRepository;

    public MeHandler(
        IIdentityProvider identityProvider,
        IUserRepository userRepository,
        ICountryRepository countryRepository,
        ICurrencyRepository currencyRepository,
        ILanguageRepository languageRepository
    )
    {
        _identityProvider = identityProvider;
        _userRepository = userRepository;
        _countryRepository = countryRepository;
        _currencyRepository = currencyRepository;
        _languageRepository = languageRepository;
    }

    public async Task<MeResponse> Handle(
        MeRequest request,
        CancellationToken cancellationToken
    )
    {
        var currentIdentityId = _identityProvider.RequireCurrent();
        var user = await _userRepository.FindByIdentityId(currentIdentityId, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException();
        }
        
        var currency = await _currencyRepository.FindById(user.Profile.CurrencyId, cancellationToken);
        
        if (currency == null)
        {
            throw new InternalErrorException();
        }
        
        var userDto = new UserDto
        {
            Id = user.Id,
            IdentityId = user.IdentityId,
            Username = user.Username,
            Email = user.Email,
            JoinedAt = user.JoinedAt,
            Profile = new ProfileDto
            {
                CurrencyId = currency.Id,
                CurrencyName = currency.Name,
            }
        };
        
        return new MeResponse(userDto);
    }
}