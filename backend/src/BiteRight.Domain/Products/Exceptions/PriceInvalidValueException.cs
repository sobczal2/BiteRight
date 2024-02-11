using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Products.Exceptions;

public class PriceInvalidValueException : BusinessRuleDomainException
{
    public PriceInvalidValueException(
        decimal minValue,
        decimal maxValue
    )
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    public decimal MinValue { get; }
    public decimal MaxValue { get; }
}