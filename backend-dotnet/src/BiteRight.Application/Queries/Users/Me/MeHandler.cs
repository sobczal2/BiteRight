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

    public MeHandler(
        IIdentityProvider identityProvider,
        IUserRepository userRepository
    )
    {
        _identityProvider = identityProvider;
        _userRepository = userRepository;
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
        
        var userDto = new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            JoinedAt = user.JoinedAt,
        };
        
        return new MeResponse(userDto);
    }
}