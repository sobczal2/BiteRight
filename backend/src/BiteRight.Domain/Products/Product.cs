using BiteRight.Domain.Categories;
using BiteRight.Domain.Common;
using BiteRight.Domain.Users;

namespace BiteRight.Domain.Products;

public class Product : AggregateRoot<ProductId>
{
    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public Price? Price { get; private set; }
    public ExpirationDate ExpirationDate { get; private set; }
    public CategoryId CategoryId { get; private set; }
    public virtual Category Category { get; private set; }
    public AddedDateTime AddedDateTime { get; private set; }
    public AmountId AmountId { get; private set; }
    public virtual Amount Amount { get; private set; }
    public UserId UserId { get; private set; }
    public virtual User User { get; private set; }

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
        AmountId = default!;
        Amount = default!;
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
        Amount amount,
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
        AmountId = amount.Id;
        Amount = amount;
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
        Amount amount,
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
            amount,
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