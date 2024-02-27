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
using BiteRight.Application.Dtos.Users;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;

#endregion

namespace BiteRight.Application.Queries.Users.Me;

public class MeHandler : QueryHandlerBase<MeRequest, MeResponse>
{
    private readonly IIdentityProvider _identityProvider;
    private readonly IUserRepository _userRepository;

    public MeHandler(
        IIdentityProvider identityProvider,
        IUserRepository userRepository
    )
    {
        _identityProvider = identityProvider;
        _userRepository = userRepository;
    }

    protected override async Task<MeResponse> HandleImpl(
        MeRequest request,
        CancellationToken cancellationToken
    )
    {
        var currentIdentityId = _identityProvider.RequireCurrent();
        var user = await _userRepository.FindByIdentityId(currentIdentityId, cancellationToken);

        if (user == null) throw new NotFoundException();

        return new MeResponse(UserDto.FromDomain(user));
    }
}