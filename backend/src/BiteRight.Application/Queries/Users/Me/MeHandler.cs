// # ==============================================================================
// # Solution: BiteRight
// # File: MeHandler.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Common;
using BiteRight.Application.Common.Exceptions;
using BiteRight.Application.Dtos.Currencies;
using BiteRight.Application.Dtos.Users;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;

#endregion

namespace BiteRight.Application.Queries.Users.Me;

public class MeHandler : QueryHandlerBase<MeRequest, MeResponse>
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IIdentityProvider _identityProvider;
    private readonly IUserRepository _userRepository;

    public MeHandler(
        IIdentityProvider identityProvider,
        IUserRepository userRepository,
        ICurrencyRepository currencyRepository
    )
    {
        _identityProvider = identityProvider;
        _userRepository = userRepository;
        _currencyRepository = currencyRepository;
    }

    protected override async Task<MeResponse> HandleImpl(
        MeRequest request,
        CancellationToken cancellationToken
    )
    {
        var currentIdentityId = _identityProvider.RequireCurrent();
        var user = await _userRepository.FindByIdentityId(currentIdentityId, cancellationToken);

        if (user == null) throw new NotFoundException();

        var currency = await _currencyRepository.FindById(user.Profile.CurrencyId, cancellationToken);

        if (currency == null) throw new InternalErrorException();

        var userDto = new UserDto
        {
            Id = user.Id,
            IdentityId = user.IdentityId,
            Username = user.Username,
            Email = user.Email,
            JoinedAt = user.JoinedAt.Value,
            Profile = new ProfileDto
            {
                Currency = CurrencyDto.FromDomain(currency),
                TimeZoneId = user.Profile.TimeZone.Id
            }
        };

        return new MeResponse(userDto);
    }
}