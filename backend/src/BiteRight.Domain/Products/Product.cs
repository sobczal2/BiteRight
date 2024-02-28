// # ==============================================================================
// # Solution: BiteRight
// # File: Product.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Domain.Categories;
using BiteRight.Domain.Common;
using BiteRight.Domain.Units;
using BiteRight.Domain.Users;

#endregion

namespace BiteRight.Domain.Products;

public class Product : AggregateRoot<ProductId>
{
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
        Amount = default!;
        CreatedById = default!;
        CreatedBy = default!;
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
        UserId createdById
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
        Amount = amount;
        DisposedState = disposedState;
        CreatedById = createdById;
        CreatedBy = default!;
    }

    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public virtual Price? Price { get; private set; }
    public ExpirationDate ExpirationDate { get; private set; }
    public CategoryId CategoryId { get; private set; }
    public virtual Category Category { get; private set; }
    public AddedDateTime AddedDateTime { get; private set; }
    public virtual Amount Amount { get; }
    public UserId CreatedById { get; private set; }
    public virtual User CreatedBy { get; private set; }
    public DisposedState DisposedState { get; private set; }

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
        return DisposedState.Value;
    }

    public void Dispose(
        DateTime currentDateTime
    )
    {
        DisposedState = DisposedState.CreateDisposed(
            currentDateTime
        );
    }

    public void Restore()
    {
        DisposedState = DisposedState.CreateNotDisposed();
    }

    public void UpdateName(
        string name
    )
    {
        Name = Name.Create(name);
    }

    public void UpdateDescription(
        string description
    )
    {
        Description = Description.Create(description);
    }

    public void UpdatePrice(
        double priceValue,
        Guid priceCurrencyId
    )
    {
        if (Price is null)
        {
            Price = Price.Create(priceValue, priceCurrencyId, Id);
        }
        else
        {
            Price.UpdateValue(priceValue);
            Price.UpdateCurrency(priceCurrencyId);
        }
    }

    public void ClearPrice()
    {
        Price = null;
    }

    // TODO: this should be handled differently also taking creation into account
    public void UpdateExpirationDate(
        ExpirationDate expirationDate
    )
    {
        ExpirationDate = expirationDate;
    }

    public void UpdateCategory(
        Guid categoryId
    )
    {
        CategoryId = categoryId;
    }

    public void UpdateAmount(
        double amountCurrentValue,
        double amountMaxValue,
        UnitId amountUnitId
    )
    {
        Amount.UpdateValue(
            amountCurrentValue,
            amountMaxValue
        );

        Amount.UpdateUnit(
            amountUnitId
        );
    }
}