// # ==============================================================================
// # Solution: BiteRight
// # File: PriceInvalidValueException.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Domain.Common.Exceptions;

#endregion

namespace BiteRight.Domain.Products.Exceptions;

public class PriceInvalidValueException : BusinessRuleDomainException
{
    public PriceInvalidValueException(
        double minValue,
        double maxValue
    )
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    public double MinValue { get; }
    public double MaxValue { get; }
}