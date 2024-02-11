using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Products.Exceptions;

public class NameInvalidLengthException : BusinessRuleDomainException
{
    public NameInvalidLengthException(
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