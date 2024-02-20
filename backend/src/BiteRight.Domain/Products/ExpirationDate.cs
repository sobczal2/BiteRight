// # ==============================================================================
// # Solution: BiteRight
// # File: ExpirationDate.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using System.Collections.Generic;
using BiteRight.Domain.Common;
using BiteRight.Domain.Products.Exceptions;

#endregion

namespace BiteRight.Domain.Products;

public class ExpirationDate : ValueObject
{
    public enum ExpirationDateKind
    {
        Unknown = 0,
        Infinite = 1,
        BestBefore = 2,
        UseBy = 3
    }

    // EF Core
    private ExpirationDate()
    {
        Value = default!;
        Kind = default!;
    }

    private ExpirationDate(
        DateOnly value,
        ExpirationDateKind kind
    )
    {
        Value = value;
        Kind = kind;
    }

    public DateOnly Value { get; }
    public ExpirationDateKind Kind { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private static ExpirationDate Create(
        DateOnly value,
        ExpirationDateKind kind
    )
    {
        Validate(value, kind);

        return new ExpirationDate(value, kind);
    }

    public static ExpirationDate CreateBestBefore(
        DateOnly value
    )
    {
        return Create(value, ExpirationDateKind.BestBefore);
    }

    public static ExpirationDate CreateUseBy(
        DateOnly value
    )
    {
        return Create(value, ExpirationDateKind.UseBy);
    }

    public static ExpirationDate CreateUnknown()
    {
        return Create(DateOnly.MinValue, ExpirationDateKind.Unknown);
    }

    public static ExpirationDate CreateInfinite()
    {
        return Create(DateOnly.MaxValue, ExpirationDateKind.Infinite);
    }

    public static ExpirationDate CreateSkipValidation(
        DateOnly value,
        ExpirationDateKind kind
    )
    {
        return new ExpirationDate(value, kind);
    }

    private static void Validate(
        DateOnly value,
        ExpirationDateKind kind
    )
    {
        if (kind == ExpirationDateKind.Infinite && value != DateOnly.MaxValue)
            throw new ExpirationDateInfiniteValueException();
    }

    public DateOnly? GetDateIfKnown()
    {
        return Kind switch
        {
            ExpirationDateKind.Unknown => null,
            ExpirationDateKind.Infinite => null,
            _ => Value
        };
    }
}