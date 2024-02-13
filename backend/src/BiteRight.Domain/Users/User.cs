// # ==============================================================================
// # Solution: BiteRight
// # File: User.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Domain.Common;
using BiteRight.Domain.Currencies;

#endregion

namespace BiteRight.Domain.Users;

public class User : AggregateRoot<UserId>
{
    // EF Core
    private User()
    {
        IdentityId = default!;
        Username = default!;
        Email = default!;
        JoinedAt = default!;
        Profile = default!;
    }

    private User(
        UserId id,
        IdentityId identityId,
        Username username,
        Email email,
        JoinedAt joinedAt,
        Profile profile
    )
        : base(id)
    {
        IdentityId = identityId;
        Username = username;
        Email = email;
        JoinedAt = joinedAt;
        Profile = profile;
    }

    public IdentityId IdentityId { get; private set; }
    public Username Username { get; private set; }
    public Email Email { get; private set; }
    public JoinedAt JoinedAt { get; private set; }
    public virtual Profile Profile { get; }

    public static User Create(
        IdentityId identityId,
        Username username,
        Email email,
        Profile profile,
        DateTime currentDateTime,
        UserId? id = null
    )
    {
        var user = new User(
            id ?? new UserId(),
            identityId,
            username,
            email,
            JoinedAt.Create(currentDateTime),
            profile
        );

        return user;
    }

    public void UpdateProfile(
        CurrencyId currencyId,
        TimeZoneInfo timeZone
    )
    {
        Profile.Update(
            currencyId,
            timeZone
        );
    }
}