using BiteRight.Domain.Common.Exceptions;

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