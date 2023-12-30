using BiteRight.Domain.Common;

namespace BiteRight.Domain.Languages.Events;

public class LanguageCreatedEvent : DomainEvent
{
    public LanguageId LanguageId { get; }
    
    public LanguageCreatedEvent(
        DateTime occurredOn,
        Guid correlationId,
        LanguageId languageId
    )
        : base(occurredOn, correlationId)
    {
        LanguageId = languageId;
    }
}