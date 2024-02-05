using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Common;
using BiteRight.Domain.Users;

namespace BiteRight.Domain.Products;

public class Product : AggregateRoot<ProductId>
{
    public Name Name { get; }
    public Description Description { get; }
    public Price? Price { get; }
    public ExpirationDate ExpirationDate { get; }
    public AddedDateTime AddedDateTime { get; }
    public Usage Usage { get; }
    public UserId UserId { get; }
    public virtual User User { get; }

    // EF Core
    private Product()
    {
        Name = default!;
        Description = default!;
        Price = default!;
        ExpirationDate = default!;
        AddedDateTime = default!;
        Usage = default!;
        UserId = default!;
        User = default!;
    }

    private Product(
        ProductId id,
        Name name,
        Description description,
        Price? price,
        ExpirationDate expirationDate,
        AddedDateTime addedDateTime,
        Usage usage,
        User user
    )
        : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        ExpirationDate = expirationDate;
        AddedDateTime = addedDateTime;
        Usage = usage;
        UserId = user.Id;
        User = user;
    }

    public static Product Create(
        Name name,
        Description description,
        Price? price,
        ExpirationDate expirationDate,
        AddedDateTime addedDateTime,
        Usage usage,
        User user,
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
            usage,
            user
        );

        product.AddDomainEvent(domainEventFactory.CreateProductCreatedEvent(product.Id));

        return product;
    }
}