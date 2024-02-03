using BiteRight.Domain.Common;

namespace BiteRight.Domain.Users.Events;

public class UserProfileUpdatedEvent : DomainEvent
{
    public IdentityId IdentityId { get; }
    
    public UserProfileUpdatedEvent(
        DateTime occurredOn,
        Guid correlationId,
        IdentityId identityId
    )
        : base(occurredOn, correlationId)
    {
        IdentityId = identityId;
    }
}