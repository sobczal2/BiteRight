using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Countries.Exceptions;

public class Alpha2CodeInvalidCharactersException : BusinessRuleDomainException
{
    public string ValidCharacters { get; }

    public Alpha2CodeInvalidCharactersException(
        string validCharacters
    )
    {
        ValidCharacters = validCharacters;
    }
}