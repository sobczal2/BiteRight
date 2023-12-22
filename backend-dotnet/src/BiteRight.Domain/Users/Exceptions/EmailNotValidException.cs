using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Users.Exceptions;

public class EmailNotValidException : BusinessRuleDomainException
{
    public EmailNotValidException(
        string message
    )
        : base(message)
    {
    }
    
    public static EmailNotValidException CreateInvalidFormat(
        string email
    )
    {
        return new EmailNotValidException(
            $"Email '{email}' is not a valid email address."
        );
    }
}