using BiteRight.Domain.Common;

namespace BiteRight.Domain.Products.Events;

public class ProductCreatedEvent : DomainEvent
{
    public ProductId ProductId { get; }

    public ProductCreatedEvent(
        DateTime occurredOn,
        Guid correlationId,
        ProductId productId
    )
        : base(occurredOn, correlationId)
    {
        ProductId = productId;
    }
}