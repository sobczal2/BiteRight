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
        DateOnly? value,
        ExpirationDateKind kind
    )
    {
        Value = value;
        Kind = kind;
    }

    public DateOnly? Value { get; }
    public ExpirationDateKind Kind { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Kind;
    }

    private static ExpirationDate Create(
        DateOnly? value,
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
        return Create(null, ExpirationDateKind.Unknown);
    }

    public static ExpirationDate CreateInfinite()
    {
        return Create(null, ExpirationDateKind.Infinite);
    }
    
    private static void Validate(
        DateOnly? value,
        ExpirationDateKind kind
    )
    {
        switch (kind)
        {
            case ExpirationDateKind.Unknown:
                if (value is not null)
                    throw new ExpirationDateUnknownValueException();
                break;
            case ExpirationDateKind.Infinite:
                if (value is not null)
                    throw new ExpirationDateInfiniteValueException();
                break;
            case ExpirationDateKind.BestBefore:
                if (value is null)
                    throw new ExpirationDateBestBeforeValueException();
                break;
            case ExpirationDateKind.UseBy:
                if (value is null)
                    throw new ExpirationDateUseByValueException();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
        }
    }
}