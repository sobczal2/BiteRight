// # ==============================================================================
// # Solution: BiteRight
// # File: DescriptionInvalidLengthException.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Domain.Common.Exceptions;

#endregion

namespace BiteRight.Domain.Products.Exceptions;

public class DescriptionInvalidLengthException
    : BusinessRuleDomainException
{
    public DescriptionInvalidLengthException(
        int maxLength
    )
    {
        MaxLength = maxLength;
    }

    public int MaxLength { get; }
}