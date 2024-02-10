using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Categories;
using BiteRight.Domain.Common;
using BiteRight.Domain.Users;

namespace BiteRight.Domain.Products;

public class Product : AggregateRoot<ProductId>
{
    public Name Name { get; }
    public Description Description { get; }
    public Price? Price { get; }
    public ExpirationDate ExpirationDate { get; }
    public CategoryId CategoryId { get; }
    public virtual Category Category { get; }
    public AddedDateTime AddedDateTime { get; }
    public Consumption Consumption { get; }
    public UserId UserId { get; }
    public virtual User User { get; }
    public DisposedState DisposedState { get; private set; }

    // EF Core
    private Product()
    {
        Name = default!;
        Description = default!;
        Price = default!;
        ExpirationDate = default!;
        CategoryId = default!;
        Category = default!;
        AddedDateTime = default!;
        Consumption = default!;
        UserId = default!;
        User = default!;
        DisposedState = default!;
    }

    private Product(
        ProductId id,
        Name name,
        Description description,
        Price? price,
        ExpirationDate expirationDate,
        CategoryId categoryId,
        AddedDateTime addedDateTime,
        Consumption consumption,
        DisposedState disposedState,
        UserId userId
    )
        : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        ExpirationDate = expirationDate;
        CategoryId = categoryId;
        Category = default!;
        AddedDateTime = addedDateTime;
        Consumption = consumption;
        DisposedState = disposedState;
        UserId = userId;
        User = default!;
    }

    public static Product Create(
        Name name,
        Description description,
        Price? price,
        ExpirationDate expirationDate,
        CategoryId categoryId,
        UserId userId,
        DateTime currentDateTime,
        ProductId? id = null
    )
    {
        var product = new Product(
            id ?? new ProductId(),
            name,
            description,
            price,
            expirationDate,
            categoryId,
            AddedDateTime.Create(currentDateTime),
            Consumption.CreateFull(),
            DisposedState.CreateNotDisposed(),
            userId
        );

        return product;
    }

    public bool IsDisposed()
    {
        return DisposedState.Disposed;
    }

    public void SetDisposed(DateTime currentDateTime)
    {
        DisposedState = DisposedState.CreateDisposed(
            currentDateTime
        );
    }
}