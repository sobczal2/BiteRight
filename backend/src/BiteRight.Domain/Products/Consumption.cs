using BiteRight.Domain.Common;
using BiteRight.Domain.Products.Exceptions;

namespace BiteRight.Domain.Products;

public class Consumption : ValueObject
{
    public double Amount { get; }
    public static Consumption All => MaxAmount;
    public static Consumption None => MinAmount;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
    }

    private Consumption(
        double amount
    )
    {
        Amount = amount;
    }

    public static Consumption Create(
        double amount
    )
    {
        Validate(amount);

        return new Consumption(amount);
    }

    public static Consumption CreateSkipValidation(
        double amount
    )
    {
        return new Consumption(amount);
    }

    public static Consumption CreateEmpty()
    {
        return Create(MaxAmount);
    }

    public static Consumption CreateFull()
    {
        return Create(MinAmount);
    }

    private const double MinAmount = 0.0;
    private const double MaxAmount = 1.0;

    private static void Validate(
        double amount
    )
    {
        if (amount is < MinAmount or > MaxAmount)
        {
            throw new ConsumptionInvalidAmountException(MinAmount, MaxAmount);
        }
    }

    public double GetPercentage()
    {
        return Amount * 100;
    }
    
    public static implicit operator double(
        Consumption consumption
    )
    {
        return consumption.Amount;
    }
    
    public static implicit operator Consumption(
        double amount
    )
    {
        return Create(amount);
    }
}