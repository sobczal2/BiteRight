using BiteRight.Domain.Common;

namespace BiteRight.Domain.Users.Events;

public class UserCreatedEvent : DomainEvent
{
    public UserId UserId { get; }
    public IdentityId IdentityId { get; }

    public UserCreatedEvent(
        DateTime occurredOn,
        Guid correlationId,
        UserId userId,
        IdentityId identityId
    )
        : base(occurredOn, correlationId)
    {
        UserId = userId;
        IdentityId = identityId;
    }
}