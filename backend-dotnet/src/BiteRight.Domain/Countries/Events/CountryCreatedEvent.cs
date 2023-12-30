using BiteRight.Domain.Common;

namespace BiteRight.Domain.Countries.Events;

public class CountryCreatedEvent : DomainEvent
{
    public CountryId CountryId { get; }
    
    public CountryCreatedEvent(
        DateTime occurredOn,
        Guid correlationId,
        CountryId countryId
    )
        : base(occurredOn, correlationId)
    {
        CountryId = countryId;
    }
}