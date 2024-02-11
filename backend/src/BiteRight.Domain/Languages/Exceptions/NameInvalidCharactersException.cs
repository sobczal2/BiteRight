using BiteRight.Domain.Common.Exceptions;

namespace BiteRight.Domain.Languages.Exceptions;

public class NameInvalidCharactersException : BusinessRuleDomainException
{
    public NameInvalidCharactersException(
        string validCharacters
    )
    {
        ValidCharacters = validCharacters;
    }

    public string ValidCharacters { get; }
}