// # ==============================================================================
// # Solution: BiteRight
// # File: Email.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Collections.Generic;
using System.Net.Mail;
using BiteRight.Domain.Common;
using BiteRight.Domain.Users.Exceptions;

#endregion

namespace BiteRight.Domain.Users;

public class Email : ValueObject
{
    private readonly MailAddress _mailAddress;

    private Email(
        MailAddress mailAddress
    )
    {
        _mailAddress = mailAddress;
    }

    public string Value => _mailAddress.Address;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static Email Create(
        string value
    )
    {
        if (!MailAddress.TryCreate(value, out var mailAddress)) throw new EmailNotValidException();

        return new Email(mailAddress);
    }

    public static Email CreateSkipValidation(
        string value
    )
    {
        return new Email(new MailAddress(value));
    }

    public static implicit operator string(Email email)
    {
        return email.Value;
    }
}