// # ==============================================================================
// # Solution: BiteRight
// # File: Price.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Collections.Generic;
using BiteRight.Domain.Common;
using BiteRight.Domain.Currencies;
using BiteRight.Domain.Products.Exceptions;

#endregion

namespace BiteRight.Domain.Products;

public class Price : Entity<PriceId>
{
    private const double MinValue = 0;
    private const double MaxValue = 1e6;

    // EF Core
    private Price()
    {
        Value = default!;
        CurrencyId = default!;
        Currency = default!;
        ProductId = default!;
        Product = default!;
    }

    private Price(
        PriceId id,
        double value,
        CurrencyId currencyId,
        ProductId productId
    )
        : base(id)
    {
        Value = value;
        CurrencyId = currencyId;
        Currency = default!;
        ProductId = productId;
        Product = default!;
    }

    public double Value { get; private set; }
    public CurrencyId CurrencyId { get; private set; }
    public virtual Currency Currency { get; }
    public ProductId ProductId { get; }
    public virtual Product Product { get; }

    public static Price Create(
        double value,
        CurrencyId currencyId,
        ProductId productId,
        PriceId? id = null
    )
    {
        Validate(value, currencyId, productId);

        return new Price(
            id ?? new PriceId(),
            value,
            currencyId,
            productId
        );
    }

    private static void Validate(
        double value,
        CurrencyId currencyId,
        ProductId productId
    )
    {
        if (value is < MinValue or > MaxValue) throw new PriceInvalidValueException(MinValue, MaxValue);
    }

    public static implicit operator double(
        Price price
    )
    {
        return price.Value;
    }

    public void UpdateValue(
        double value
    )
    {
        Validate(value, CurrencyId, ProductId);
        Value = value;
    }

    public void UpdateCurrency(
        CurrencyId currencyId
    )
    {
        Validate(Value, currencyId, ProductId);
        CurrencyId = currencyId;
    }
}