using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Product.Exceptions;

public class PriceInvalidValueException : BusinessRuleDomainException
{
    public decimal MinValue { get; }
    public decimal MaxValue { get; }
    
    public PriceInvalidValueException(
        decimal minValue,
        decimal maxValue
    )
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }
}