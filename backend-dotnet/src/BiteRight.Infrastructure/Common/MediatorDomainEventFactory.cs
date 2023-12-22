using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Users;
using BiteRight.Domain.Users.Events;

namespace BiteRight.Infrastructure.Common;

public class MediatorDomainEventFactory : IDomainEventFactory
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICorrelationIdAccessor _correlationIdAccessor;

    public MediatorDomainEventFactory(
        IDateTimeProvider dateTimeProvider,
        ICorrelationIdAccessor correlationIdAccessor
    )
    {
        _dateTimeProvider = dateTimeProvider;
        _correlationIdAccessor = correlationIdAccessor;
    }

    public UserCreatedEvent CreateUserCreatedEvent(
        UserId userId,
        IdentityId identityId
    )
    {
        return new UserCreatedEvent(
            _dateTimeProvider.UtcNow,
            _correlationIdAccessor.CorrelationId,
            userId,
            identityId
        );
    }
}