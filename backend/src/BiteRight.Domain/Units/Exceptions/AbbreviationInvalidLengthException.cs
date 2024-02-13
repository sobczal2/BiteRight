// # ==============================================================================
// # Solution: BiteRight
// # File: AbbreviationInvalidLengthException.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Domain.Common.Exceptions;

#endregion

namespace BiteRight.Domain.Units.Exceptions;

public class AbbreviationInvalidLengthException : BusinessRuleDomainException
{
    public AbbreviationInvalidLengthException(
        int minLength,
        int maxLength
    )
    {
        MinLength = minLength;
        MaxLength = maxLength;
    }

    public int MinLength { get; }
    public int MaxLength { get; }
}