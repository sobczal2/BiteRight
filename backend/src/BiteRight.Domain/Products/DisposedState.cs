// # ==============================================================================
// # Solution: BiteRight
// # File: DisposedState.cs
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

public class DisposedState : ValueObject
{
    // EF Core
    private DisposedState()
    {
        Value = default!;
        DateTime = default!;
    }

    private DisposedState(
        bool value,
        DateTime? dateTime
    )
    {
        Value = value;
        DateTime = dateTime;
    }

    public bool Value { get; }

    public DateTime? DateTime { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return DateTime!;
    }

    private static DisposedState Create(
        bool disposed,
        DateTime? disposedDate
    )
    {
        Validate(disposed, disposedDate);
        return new DisposedState(disposed, disposedDate);
    }

    private static void Validate(
        bool disposed,
        DateTime? disposedDate
    )
    {
        if (!disposed)
        {
            if (disposedDate.HasValue) throw new DisposedStateInvalidDisposedDateValueException();
        }
        else
        {
            if (disposedDate is not { Kind: DateTimeKind.Utc })
                throw new DisposedStateInvalidDisposedDateValueException();
        }
    }

    public static DisposedState CreateNotDisposed()
    {
        return Create(false, null);
    }

    public static DisposedState CreateDisposed(
        DateTime disposedDate
    )
    {
        return Create(true, disposedDate);
    }
}