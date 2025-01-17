// # ==============================================================================
// # Solution: BiteRight
// # File: Name.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Collections.Generic;
using System.Text.RegularExpressions;
using BiteRight.Domain.Common;
using BiteRight.Domain.Countries.Exceptions;
using BiteRight.Utils;

#endregion

namespace BiteRight.Domain.Countries;

public class Name : ValueObject
{
    private const int MinLength = 3;
    private const int MaxLength = 100;

    private static readonly Regex ValidCharacters = CommonRegexes.LettersWithSpaces;

    private Name(
        string value
    )
    {
        Value = value;
    }

    public string Value { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static Name Create(
        string value
    )
    {
        Validate(value);

        return new Name(value);
    }

    public static Name CreateSkipValidation(
        string value
    )
    {
        return new Name(value);
    }

    private static void Validate(
        string value
    )
    {
        if (string.IsNullOrWhiteSpace(value)) throw new NameEmptyException();

        if (value.Length is < MinLength or > MaxLength) throw new NameInvalidLengthException(MinLength, MaxLength);

        if (!ValidCharacters.IsMatch(value)) throw new NameInvalidCharactersException(ValidCharacters.ToString());
    }

    public static implicit operator string(
        Name name
    )
    {
        return name.Value;
    }
}