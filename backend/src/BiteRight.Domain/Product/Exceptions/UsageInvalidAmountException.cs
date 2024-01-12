using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Product.Exceptions;

public class UsageInvalidAmountException : BusinessRuleDomainException
{
    public double MinAmount { get; }
    public double MaxAmount { get; }
    
    public UsageInvalidAmountException(
        double minAmount,
        double maxAmount
    )
    {
        MinAmount = minAmount;
        MaxAmount = maxAmount;
    }
}