// # ==============================================================================
// # Solution: BiteRight
// # File: TestUsers.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Domain.Users;
using BiteRight.Infrastructure.Configuration.Currencies;
using BiteRight.Infrastructure.Database;

#endregion

namespace BiteRight.Web.Tests.Integration;

public static class TestUsers
{
    private static readonly UserId EmailNotVerifiedUserId = new(Guid.Parse("047D8F7F-A83C-465F-97B4-DF5E99588279"));

    public static readonly User EmailNotVerifiedUser = User.Create(
        IdentityId.Create("EmailNotVerifiedUserIdentityId"),
        Username.Create("EmailNotVerifiedUser"),
        Email.Create("not-verified@test.com"),
        Profile.Create(EmailNotVerifiedUserId, CurrencyConfiguration.USD.Id, TimeZoneInfo.Utc),
        new DateTime(2020, 10, 10).ToUniversalTime(),
        EmailNotVerifiedUserId
    );

    private static readonly UserId EmailVerifiedUserId = new(Guid.Parse("81CD8CC8-5531-4F7E-9D99-F77173B27882"));

    public static readonly User EmailVerifiedUser = User.Create(
        IdentityId.Create("EmailVerifiedUserIdentityId"),
        Username.Create("EmailVerifiedUser"),
        Email.Create("verified@test.com"),
        Profile.Create(EmailVerifiedUserId, CurrencyConfiguration.USD.Id, TimeZoneInfo.Utc),
        new DateTime(2020, 10, 10).ToUniversalTime(),
        EmailVerifiedUserId
    );

    private static readonly UserId NotFoundUserId = new(Guid.Parse("EEAFD93F-1ABD-4E1A-BCA1-DAD4440D9AE3"));

    public static readonly User NotFoundUser = User.Create(
        IdentityId.Create("NotFoundUserIdentityId"),
        Username.Create("NotFoundUser"),
        Email.Create("not-found@test.com"),
        Profile.Create(NotFoundUserId, CurrencyConfiguration.USD.Id, TimeZoneInfo.Utc),
        new DateTime(2020, 10, 10).ToUniversalTime(),
        NotFoundUserId
    );

    private static readonly UserId IdentityProviderDownUserId = new(Guid.Parse("E718C63D-520E-483F-94B8-D1426FB6950A"));

    public static readonly User IdentityProviderDownUser = User.Create(
        IdentityId.Create("IdentityProviderDownUserIdentityId"),
        Username.Create("IdentityProviderDownUser"),
        Email.Create("identity-provider-down@test.com"),
        Profile.Create(IdentityProviderDownUserId, CurrencyConfiguration.USD.Id, TimeZoneInfo.Utc),
        new DateTime(2020, 10, 10).ToUniversalTime(),
        IdentityProviderDownUserId
    );

    private static readonly UserId OnboardedUserId = new(Guid.Parse("BE3C0D76-8ED5-4B40-9191-B285DEC976D2"));

    public static readonly User OnboardedUser = User.Create(
        IdentityId.Create("OnboardedUserIdentityId"),
        Username.Create("OnboardedUser"),
        Email.Create("onboarded@test.com"),
        Profile.Create(OnboardedUserId, CurrencyConfiguration.USD.Id, TimeZoneInfo.Utc),
        new DateTime(2020, 10, 10).ToUniversalTime(),
        OnboardedUserId
    );

    public static void SeedOnboardedUser(
        AppDbContext appDbContext
    )
    {
        appDbContext.Users.Add(OnboardedUser);
        appDbContext.SaveChanges();
    }
}