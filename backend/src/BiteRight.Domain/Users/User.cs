using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Common;
using BiteRight.Domain.Countries;
using BiteRight.Domain.Currencies;
using BiteRight.Domain.Languages;

namespace BiteRight.Domain.Users;

public class User : AggregateRoot<UserId>
{
    public IdentityId IdentityId { get; private set; }
    public Username Username { get; private set; }
    public Email Email { get; private set; }
    public JoinedAt JoinedAt { get; private set; }
    public ProfileId ProfileId { get; private set; }
    public virtual Profile Profile { get; private set; }

    // EF Core
    private User()
    {
        IdentityId = default!;
        Username = default!;
        Email = default!;
        JoinedAt = default!;
        Profile = default!;
        ProfileId = default!;
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
        ProfileId = profile.Id;
    }

    public static User Create(
        IdentityId identityId,
        Username username,
        Email email,
        Profile profile,
        IDateTimeProvider dateTimeProvider,
        IDomainEventFactory domainEventFactory
    )
    {
        var user = new User(
            new UserId(Guid.NewGuid()),
            identityId,
            username,
            email,
            JoinedAt.Create(dateTimeProvider.UtcNow),
            profile
        );

        user.AddDomainEvent(
            domainEventFactory.CreateUserCreatedEvent(
                user.IdentityId
            )
        );

        return user;
    }

    public void UpdateProfile(
        CurrencyId currencyId,
        IDomainEventFactory domainEventFactory
    )
    {
        Profile.Update(
            currencyId
        );

        AddDomainEvent(
            domainEventFactory.CreateUserProfileUpdatedEvent(
                IdentityId
            )
        );
    }
}