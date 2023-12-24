using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Currency.Exceptions;

public class SymbolInvalidLengthException : BusinessRuleDomainException
{
    public int MinLength { get; }
    public int MaxLength { get; }
    
    public SymbolInvalidLengthException(
        int minLength,
        int maxLength
    )
    {
        MinLength = minLength;
        MaxLength = maxLength;
    }
}