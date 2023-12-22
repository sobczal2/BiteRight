using System.Net.Mail;
using BiteRight.Domain.Common;
using BiteRight.Domain.Users.Exceptions;

namespace BiteRight.Domain.Users;

public class Email : ValueObject
{
    public string Value => _mailAddress.Address;
    private readonly MailAddress _mailAddress;

    private Email(
        MailAddress mailAddress
    )
    {
        _mailAddress = mailAddress;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public static Email Create(
        string value
    )
    {
        if(!MailAddress.TryCreate(value, out var mailAddress))
        {
            throw new EmailNotValidException();
        }

        return new Email(mailAddress);
    }
    
    public static Email CreateSkipValidation(
        string value
    )
    {
        return new Email(new MailAddress(value));
    }
}