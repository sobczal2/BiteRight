using BiteRight.Domain.Common;
using BiteRight.Domain.Currencies;
using BiteRight.Domain.Products.Exceptions;

namespace BiteRight.Domain.Products;

public class Price : Entity<PriceId>
{
    public decimal Value { get; }
    public CurrencyId CurrencyId { get; }
    public virtual Currency Currency { get; }

    // EF Core
    private Price()
    {
        Value = default!;
        CurrencyId = default!;
        Currency = default!;
    }

    private Price(
        decimal value,
        Currency currency
    )
    {
        Value = value;
        CurrencyId = currency.Id;
        Currency = currency;
    }

    public static Price Create(
        decimal value,
        Currency currency
    )
    {
        Validate(value, currency);

        return new Price(value, currency);
    }

    public static Price CreateSkipValidation(
        decimal value,
        Currency currency
    )
    {
        return new Price(value, currency);
    }

    private const decimal MinValue = 0.00m;
    private const decimal MaxValue = 1e6m;

    private static void Validate(
        decimal value,
        Currency currency
    )
    {
        if (value is < MinValue or > MaxValue)
        {
            throw new PriceInvalidValueException(MinValue, MaxValue);
        }
    }

    public static implicit operator decimal(
        Price price
    ) => price.Value;
}