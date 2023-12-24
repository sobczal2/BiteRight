using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Currency.Exceptions;

public class CodeInvalidLengthException : BusinessRuleDomainException
{
    public int ExactLength { get; }
    
    public CodeInvalidLengthException(
        int exactLength
    )
    {
        ExactLength = exactLength;
    }
}