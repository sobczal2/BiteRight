// # ==============================================================================
// # Solution: BiteRight
// # File: TestIdentityManager.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Users;

#endregion

namespace BiteRight.Web.Tests.Integration.Dependencies;

public class TestIdentityManager : IIdentityManager
{
    public Task<(Email email, bool isVerified)> GetEmail(
        IdentityId identityId,
        CancellationToken cancellationToken = default
    )
    {
        if (Equals(identityId, TestUsers.EmailNotVerifiedUser.IdentityId))
            return Task.FromResult((TestUsers.EmailNotVerifiedUser.Email, false));

        if (Equals(identityId, TestUsers.EmailVerifiedUser.IdentityId))
            return Task.FromResult((TestUsers.EmailVerifiedUser.Email, true));

        if (Equals(identityId, TestUsers.NotFoundUser.IdentityId))
            throw new InvalidOperationException("User not found.");

        if (Equals(identityId, TestUsers.IdentityProviderDownUser.IdentityId))
            throw new HttpRequestException("Identity provider down.");

        if (Equals(identityId, TestUsers.OnboardedUser.IdentityId))
            return Task.FromResult((TestUsers.OnboardedUser.Email, true));

        throw new ArgumentException("Unexpected user id");
    }
}