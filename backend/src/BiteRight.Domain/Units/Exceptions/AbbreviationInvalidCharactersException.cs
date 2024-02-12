// # ==============================================================================
// # Solution: BiteRight
// # File: AbbreviationInvalidCharactersException.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Domain.Common.Exceptions;

#endregion

namespace BiteRight.Domain.Units.Exceptions;

public class AbbreviationInvalidCharactersException : BusinessRuleDomainException
{
    public AbbreviationInvalidCharactersException(
        string validCharacters
    )
    {
        ValidCharacters = validCharacters;
    }

    public string ValidCharacters { get; }
}