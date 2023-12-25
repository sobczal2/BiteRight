using BiteRight.Domain.Common;

namespace BiteRight.Domain.Categories.Events;

public class CategoryCreatedEvent : DomainEvent
{
    public CategoryId CategoryId { get; }

    public CategoryCreatedEvent(
        DateTime occurredOn,
        Guid correlationId,
        CategoryId categoryId
    )
        : base(occurredOn, correlationId)
    {
        CategoryId = categoryId;
    }
}