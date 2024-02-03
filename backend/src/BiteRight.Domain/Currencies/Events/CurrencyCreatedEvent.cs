using BiteRight.Domain.Common;

namespace BiteRight.Domain.Currencies.Events;

public class CurrencyCreatedEvent : DomainEvent
{
    public CurrencyId CurrencyId { get; }

    public CurrencyCreatedEvent(
        DateTime occurredOn,
        Guid correlationId,
        CurrencyId currencyId
    )
        : base(occurredOn, correlationId)
    {
        CurrencyId = currencyId;
    }
}