namespace BiteRight.Domain.Common.Exceptions;

public class BusinessRuleDomainException : DomainException
{
    public BusinessRuleDomainException(
        string message
    )
        : base(message)
    {
    }
}