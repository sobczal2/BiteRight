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
using BiteRight.Domain.Products.Exceptions;
using BiteRight.Utils;

#endregion

namespace BiteRight.Domain.Products;

public class Name : ValueObject
{
    private const int MinValidLength = 3;
    private const int MaxValidLength = 64;

    private static readonly Regex ValidCharacters = CommonRegexes.AlphanumericWithSpacesAndSpecialCharacters;

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

        if (value.Length is < MinValidLength or > MaxValidLength) throw new NameInvalidLengthException(MinValidLength, MaxValidLength);

        if (!ValidCharacters.IsMatch(value)) throw new NameInvalidCharactersException(ValidCharacters.ToString());
    }

    public static implicit operator string(
        Name name
    )
    {
        return name.Value;
    }
}