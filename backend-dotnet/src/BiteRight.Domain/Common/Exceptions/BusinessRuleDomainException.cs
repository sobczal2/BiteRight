namespace BiteRight.Domain.Common.Exceptions;

public class BusinessRuleDomainException : DomainException
{

    public BusinessRuleDomainException()
        : base("Business rule violation")
    {
    }
}