// # ==============================================================================
// # Solution: BiteRight
// # File: AuthHelpers.cs
// # Author: Łukasz Sobczak
// # Created: 13-02-2024
// # ==============================================================================

#region

using System.Net.Http;
using BiteRight.Domain.Users;

#endregion

namespace BiteRight.Web.Tests.Integration.TestHelpers;

public static class AuthHelpers
{
    public static HttpRequestMessage AuthorizeAsUser(
        this HttpRequestMessage request,
        User user
    )
    {
        request.Headers.Add(TestAuthHandler.HeaderName, user.IdentityId.ToString());
        return request;
    }

    public static HttpRequestMessage AuthorizeAsEmailNotVerifiedUser(
        this HttpRequestMessage request
    )
    {
        return request.AuthorizeAsUser(TestUsers.EmailNotVerifiedUser);
    }

    public static HttpRequestMessage AuthorizeAsEmailVerifiedUser(
        this HttpRequestMessage request
    )
    {
        return request.AuthorizeAsUser(TestUsers.EmailVerifiedUser);
    }

    public static HttpRequestMessage AuthorizeAsNotFoundUser(
        this HttpRequestMessage request
    )
    {
        return request.AuthorizeAsUser(TestUsers.NotFoundUser);
    }

    public static HttpRequestMessage AuthorizeAsIdentityProviderDownUser(
        this HttpRequestMessage request
    )
    {
        return request.AuthorizeAsUser(TestUsers.IdentityProviderDownUser);
    }

    public static HttpRequestMessage AuthorizeAsOnboardedUser(
        this HttpRequestMessage request
    )
    {
        return request.AuthorizeAsUser(TestUsers.OnboardedUser);
    }
}