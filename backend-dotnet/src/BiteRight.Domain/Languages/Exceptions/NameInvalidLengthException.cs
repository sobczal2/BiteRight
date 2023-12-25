using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Languages.Exceptions;

public class NameInvalidLengthException : BusinessRuleDomainException
{
    public int MinLength { get; }
    public int MaxLength { get; }
    
    public NameInvalidLengthException(
        int minLength,
        int maxLength
    )
    {
        MinLength = minLength;
        MaxLength = maxLength;
    }
}