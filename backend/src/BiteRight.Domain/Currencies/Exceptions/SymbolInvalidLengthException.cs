// # ==============================================================================
// # Solution: BiteRight
// # File: SymbolInvalidLengthException.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Domain.Common.Exceptions;

#endregion

namespace BiteRight.Domain.Currencies.Exceptions;

public class SymbolInvalidLengthException : BusinessRuleDomainException
{
    public SymbolInvalidLengthException(
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