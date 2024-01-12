using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Product.Exceptions;

public class DescriptionInvalidLengthException
    : BusinessRuleDomainException
{
    public int MinLength { get; }
    public int MaxLength { get; }

    public DescriptionInvalidLengthException(
        int minLength,
        int maxLength
    )
    {
        MinLength = minLength;
        MaxLength = maxLength;
    }
}