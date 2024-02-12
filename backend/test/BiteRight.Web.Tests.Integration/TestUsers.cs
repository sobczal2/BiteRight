// # ==============================================================================
// # Solution: BiteRight
// # File: TestUsers.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

using BiteRight.Domain.Users;

namespace BiteRight.Web.Tests.Integration;

public static class TestUsers
{
    public const string EmailNotVerifiedUserId = "EmailNotVerifiedUserId";
    public const string EmailVerifiedUserId = "EmailVerifiedUserId";
    public const string NotFoundUserId = "NotFoundUserId";
    public const string IdentityProviderDownUserId = "IdentityProviderDownUserId";
    public static readonly Email TestEmail = Email.Create("test@test.com");
}