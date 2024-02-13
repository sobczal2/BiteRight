// # ==============================================================================
// # Solution: BiteRight
// # File: Name.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Collections.Generic;
using BiteRight.Domain.Categories.Exceptions;
using BiteRight.Domain.Common;

#endregion

namespace BiteRight.Domain.Categories;

public class Name : ValueObject
{
    private const int MinLength = 3;
    private const int MaxLength = 64;

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
    }

    public static implicit operator string(
        Name name
    )
    {
        return name.Value;
    }

    public static implicit operator Name(
        string value
    )
    {
        return Create(value);
    }
}