// # ==============================================================================
// # Solution: BiteRight
// # File: Alpha2CodeInvalidCharactersException.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Domain.Common.Exceptions;

#endregion

namespace BiteRight.Domain.Countries.Exceptions;

public class Alpha2CodeInvalidCharactersException : BusinessRuleDomainException
{
    public Alpha2CodeInvalidCharactersException(
        string validCharacters
    )
    {
        ValidCharacters = validCharacters;
    }

    public string ValidCharacters { get; }
}