// # ==============================================================================
// # Solution: BiteRight
// # File: TestIdentityManager.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Users;

namespace BiteRight.Web.Tests.Integration.Dependencies;

public class TestIdentityManager : IIdentityManager
{
    public Task<(Email email, bool isVerified)> GetEmail(IdentityId identityId,
        CancellationToken cancellationToken = default)
    {
        return identityId.Value switch
        {
            TestUsers.EmailNotVerifiedUserId => Task.FromResult((TestUsers.TestEmail, false)),
            TestUsers.EmailVerifiedUserId => Task.FromResult((TestUsers.TestEmail, true)),
            TestUsers.NotFoundUserId =>
                // Mirrored behaviour of Auth0IdentityManager, should probably be changed
                throw new InvalidOperationException("User not found."),
            TestUsers.IdentityProviderDownUserId =>
                // Check later if correct
                throw new HttpIOException(HttpRequestError.ConnectionError),
            _ => throw new ArgumentException("Unexpected user id")
        };
    }
}