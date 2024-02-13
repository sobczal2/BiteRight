// # ==============================================================================
// # Solution: BiteRight
// # File: Profile.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Domain.Common;
using BiteRight.Domain.Currencies;

#endregion

namespace BiteRight.Domain.Users;

public class Profile : Entity<ProfileId>
{
    // EF Core
    private Profile()
    {
        UserId = default!;
        User = default!;
        CurrencyId = default!;
        Currency = default!;
        TimeZone = default!;
    }

    private Profile(
        UserId userId,
        CurrencyId currencyId,
        TimeZoneInfo timeZone,
        ProfileId id
    )
        : base(id)
    {
        UserId = userId;
        User = default!;
        CurrencyId = currencyId;
        Currency = default!;
        TimeZone = timeZone;
    }

    public UserId UserId { get; private set; }
    public virtual User User { get; private set; }
    public CurrencyId CurrencyId { get; private set; }
    public virtual Currency Currency { get; private set; }

    public TimeZoneInfo TimeZone { get; private set; }

    public static Profile Create(
        UserId userId,
        CurrencyId currencyId,
        TimeZoneInfo timeZone,
        ProfileId? id = null
    )
    {
        return new Profile(
            userId,
            currencyId,
            timeZone,
            id ?? new ProfileId()
        );
    }

    public void Update(
        CurrencyId currencyId,
        TimeZoneInfo timeZone
    )
    {
        CurrencyId = currencyId;
        TimeZone = timeZone;
    }
}