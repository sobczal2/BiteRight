using BiteRight.Domain.Common;

namespace BiteRight.Domain.Users.Events;

public class UserCreatedEvent : DomainEvent
{
    public IdentityId IdentityId { get; }

    public UserCreatedEvent(
        DateTime occurredOn,
        Guid correlationId,
        IdentityId identityId
    )
        : base(occurredOn, correlationId)
    {
        IdentityId = identityId;
    }
}