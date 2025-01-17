// # ==============================================================================
// # Solution: BiteRight
// # File: Description.cs
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

public class Description : ValueObject
{
    private const int MaxLength = 512;

    private static readonly Regex ValidCharacters = CommonRegexes.AlphanumericWithSpacesAndSpecialCharacters;

    private Description(
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

    public static Description Create(
        string value
    )
    {
        Validate(value);

        return new Description(value);
    }

    public static Description CreateSkipValidation(
        string value
    )
    {
        return new Description(value);
    }

    private static void Validate(
        string value
    )
    {
        if (value.Length > MaxLength) throw new DescriptionInvalidLengthException(MaxLength);

        if (!ValidCharacters.IsMatch(value))
            throw new DescriptionInvalidCharactersException(ValidCharacters.ToString());
    }

    public static implicit operator string(
        Description description
    )
    {
        return description.Value;
    }
}