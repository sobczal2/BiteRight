using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Products.Exceptions;

public class ConsumptionInvalidAmountException : BusinessRuleDomainException
{
    public double MinAmount { get; }
    public double MaxAmount { get; }
    
    public ConsumptionInvalidAmountException(
        double minAmount,
        double maxAmount
    )
    {
        MinAmount = minAmount;
        MaxAmount = maxAmount;
    }
}