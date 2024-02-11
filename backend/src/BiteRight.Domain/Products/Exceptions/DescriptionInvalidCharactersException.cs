using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Products.Exceptions;

public class DescriptionInvalidCharactersException : BusinessRuleDomainException
{
    public string ValidCharacters { get; }

    public DescriptionInvalidCharactersException(
        string validCharacters
    )
    {
        ValidCharacters = validCharacters;
    }
}