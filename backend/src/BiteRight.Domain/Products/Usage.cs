using BiteRight.Domain.Common;
using BiteRight.Domain.Products.Exceptions;

namespace BiteRight.Domain.Products;

public class Usage : ValueObject
{
    public double Amount { get; }
    public static Usage Empty => MinAmount;
    public static Usage Full => MaxAmount;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
    }

    private Usage(
        double amount
    )
    {
        Amount = amount;
    }

    public static Usage Create(
        double amount
    )
    {
        Validate(amount);

        return new Usage(amount);
    }

    public static Usage CreateSkipValidation(
        double amount
    )
    {
        return new Usage(amount);
    }

    public static Usage CreateEmpty()
    {
        return Create(MinAmount);
    }

    public static Usage CreateFull()
    {
        return Create(MaxAmount);
    }

    private const double MinAmount = 0.0;
    private const double MaxAmount = 1.0;

    private static void Validate(
        double amount
    )
    {
        if (amount is < MinAmount or > MaxAmount)
        {
            throw new UsageInvalidAmountException(MinAmount, MaxAmount);
        }
    }

    public double GetPercentage()
    {
        return Amount * 100;
    }

    public bool IsEmpty()
    {
        return Math.Abs(Amount - MinAmount) < double.Epsilon;
    }

    public bool IsFull()
    {
        return Math.Abs(Amount - MaxAmount) < double.Epsilon;
    }
    
    public static implicit operator double(
        Usage usage
    )
    {
        return usage.Amount;
    }
    
    public static implicit operator Usage(
        double amount
    )
    {
        return Create(amount);
    }
}