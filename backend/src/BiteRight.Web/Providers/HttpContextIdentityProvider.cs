// # ==============================================================================
// # Solution: BiteRight
// # File: HttpContextIdentityProvider.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Users;
using Microsoft.AspNetCore.Http;

#endregion

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

    public async Task<UserId> RequireCurrentUserId(
        CancellationToken cancellationToken = default
    )
    {
        return (await RequireCurrentUser(cancellationToken)).Id;
    }

    public async Task<User> RequireCurrentUser(
        CancellationToken cancellationToken = default
    )
    {
        var identityId = RequireCurrent();
        var user = await _userRepository.FindByIdentityId(identityId, cancellationToken);
        if (user is null) throw new InvalidOperationException("User not found");

        return user;
    }

    private IdentityId? GetIdentityId()
    {
        var identityName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
        return identityName is null ? null : IdentityId.Create(identityName);
    }
}