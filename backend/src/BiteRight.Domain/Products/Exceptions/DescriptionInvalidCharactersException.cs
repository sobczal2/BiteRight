// # ==============================================================================
// # Solution: BiteRight
// # File: DescriptionInvalidCharactersException.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Domain.Common.Exceptions;

#endregion

namespace BiteRight.Domain.Products.Exceptions;

public class DescriptionInvalidCharactersException : BusinessRuleDomainException
{
    public DescriptionInvalidCharactersException(
        string validCharacters
    )
    {
        ValidCharacters = validCharacters;
    }

    public string ValidCharacters { get; }
}