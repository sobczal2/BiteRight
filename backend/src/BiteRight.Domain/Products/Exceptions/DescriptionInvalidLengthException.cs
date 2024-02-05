using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Products.Exceptions;

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