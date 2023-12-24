using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Common;

namespace BiteRight.Domain.Users;

public class User : AggregateRoot<UserId>
{
    public IdentityId IdentityId { get; private set; }
    public Username Username { get; private set; }
    public Email Email { get; private set; }
    public DateTime JoinedAt { get; private set; }

    // EF Core
    private User()
    {
        IdentityId = default!;
        Username = default!;
        Email = default!;
        JoinedAt = default!;
    }

    private User(
        UserId id,
        IdentityId identityId,
        Username username,
        Email email,
        bool isEmailVerified,
        DateTime joinedAt
    )
        : base(id)
    {
        IdentityId = identityId;
        Username = username;
        Email = email;
        JoinedAt = joinedAt;
    }

    public static User Create(
        IdentityId identityId,
        Username username,
        Email email,
        bool isEmailVerified,
        IDomainEventFactory domainEventFactory,
        IDateTimeProvider dateTimeProvider
    )
    {
        var user = new User(
            new UserId(Guid.NewGuid()),
            identityId,
            username,
            email,
            isEmailVerified,
            dateTimeProvider.UtcNow
        );
        
        user.AddDomainEvent(
            domainEventFactory.CreateUserCreatedEvent(
                user.Id,
                user.IdentityId
            )
        );
        
        return user;
    }
}