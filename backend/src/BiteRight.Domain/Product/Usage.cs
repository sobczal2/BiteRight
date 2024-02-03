using BiteRight.Domain.Common;
using BiteRight.Domain.Product.Exceptions;

namespace BiteRight.Domain.Product;

public class Usage : ValueObject
{
    public double Amount { get; }
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
}