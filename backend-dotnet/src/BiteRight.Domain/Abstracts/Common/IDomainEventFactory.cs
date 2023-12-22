using BiteRight.Domain.Users;
using BiteRight.Domain.Users.Events;

namespace BiteRight.Domain.Abstracts.Common;

public interface IDomainEventFactory
{
    UserCreatedEvent CreateUserCreatedEvent(
        UserId userId,
        IdentityId identityId
    );
}