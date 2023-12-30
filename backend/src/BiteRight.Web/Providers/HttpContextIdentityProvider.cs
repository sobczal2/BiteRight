using System;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Users;
using Microsoft.AspNetCore.Http;

namespace BiteRight.Web.Providers;

public class HttpContextIdentityProvider : IIdentityProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextIdentityProvider(
        IHttpContextAccessor httpContextAccessor
    )
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public IdentityId RequireCurrent()
    {
        var identityId = GetIdentityId();
        if (identityId is null)
        {
            throw new InvalidOperationException("Auth0Id is null");
        }

        return identityId;
    }

    private IdentityId? GetIdentityId()
    {
        var identityName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
        return identityName is null ? null : IdentityId.Create(identityName);
    }
}