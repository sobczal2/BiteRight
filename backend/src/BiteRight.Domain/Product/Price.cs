using BiteRight.Domain.Common;
using BiteRight.Domain.Product.Exceptions;

namespace BiteRight.Domain.Product;

public class Price : ValueObject
{
    public decimal Value { get; }

    // TODO: add currency
    private Price(
        decimal value
    )
    {
        Value = value;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public static Price Create(
        decimal value
    )
    {
        Validate(value);
        
        return new Price(value);
    }
    
    public static Price CreateSkipValidation(
        decimal value
    )
    {
        return new Price(value);
    }
    
    private const decimal MinValue = 0.00m;
    private const decimal MaxValue = 1e6m;
    
    private static void Validate(
        decimal value
    )
    {
        if (value is < MinValue or > MaxValue)
        {
            throw new PriceInvalidValueException(MinValue, MaxValue);
        }
    }
    
    public static implicit operator decimal(Price price) => price.Value;
}