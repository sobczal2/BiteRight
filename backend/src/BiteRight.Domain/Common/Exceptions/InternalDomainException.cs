namespace BiteRight.Domain.Common.Exceptions;

public class InternalDomainException : DomainException
{
    public InternalDomainException(
        string message
    )
        : base(message)
    {
    }
}