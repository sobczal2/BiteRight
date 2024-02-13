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