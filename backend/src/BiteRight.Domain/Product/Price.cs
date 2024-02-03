using BiteRight.Domain.Common;
using BiteRight.Domain.Currencies;
using BiteRight.Domain.Product.Exceptions;

namespace BiteRight.Domain.Product;

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
        CurrencyId currencyId
    )
    {
        Value = value;
        CurrencyId = currencyId;
    }

    public static Price Create(
        decimal value,
        CurrencyId currencyId
    )
    {
        Validate(value, currencyId);

        return new Price(value, currencyId);
    }

    public static Price CreateSkipValidation(
        decimal value,
        CurrencyId currencyId
    )
    {
        return new Price(value, currencyId);
    }

    private const decimal MinValue = 0.00m;
    private const decimal MaxValue = 1e6m;

    private static void Validate(
        decimal value,
        CurrencyId currencyId
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