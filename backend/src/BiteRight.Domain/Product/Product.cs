using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Common;

namespace BiteRight.Domain.Product;

public class Product : AggregateRoot<ProductId>
{
    public Name Name { get; }
    public Description Description { get; }
    public Price? Price { get; }
    public ExpirationDate ExpirationDate { get; }
    public AddedDateTime AddedDateTime { get; }
    public Usage Usage { get; }

    // EF Core
    private Product()
    {
        Name = default!;
        Description = default!;
        Price = default!;
        ExpirationDate = default!;
        AddedDateTime = default!;
        Usage = default!;
    }

    private Product(
        ProductId id,
        Name name,
        Description description,
        Price? price,
        ExpirationDate expirationDate,
        AddedDateTime addedDateTime,
        Usage usage
    )
        : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        ExpirationDate = expirationDate;
        AddedDateTime = addedDateTime;
        Usage = usage;
    }

    public static Product Create(
        Name name,
        Description description,
        Price? price,
        ExpirationDate expirationDate,
        AddedDateTime addedDateTime,
        Usage usage,
        IDomainEventFactory domainEventFactory,
        ProductId? id = null
    )
    {
        var product = new Product(
            id ?? new ProductId(),
            name,
            description,
            price,
            expirationDate,
            addedDateTime,
            usage
        );

        product.AddDomainEvent(domainEventFactory.CreateProductCreatedEvent(product.Id));

        return product;
    }
}