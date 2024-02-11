using System;
using System.Threading.Tasks;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Users;
using Microsoft.AspNetCore.Http;

namespace BiteRight.Web.Providers;

public class HttpContextIdentityProvider : IIdentityProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;

    public HttpContextIdentityProvider(
        IHttpContextAccessor httpContextAccessor,
        IUserRepository userRepository
    )
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
    }

    public IdentityId RequireCurrent()
    {
        var identityId = GetIdentityId();
        if (identityId is null) throw new InvalidOperationException("Auth0Id is null");

        return identityId;
    }

    public async Task<UserId> RequireCurrentUserId()
    {
        return (await RequireCurrentUser()).Id;
    }

    public async Task<User> RequireCurrentUser()
    {
        var identityId = RequireCurrent();
        var user = await _userRepository.FindByIdentityId(identityId);
        if (user is null) throw new InvalidOperationException("User not found");

        return user;
    }

    private IdentityId? GetIdentityId()
    {
        var identityName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
        return identityName is null ? null : IdentityId.Create(identityName);
    }
}