// # ==============================================================================
// # Solution: BiteRight
// # File: JoinedAt.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using System.Collections.Generic;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Common;
using BiteRight.Domain.Products.Exceptions;

#endregion

namespace BiteRight.Domain.Users;

public class JoinedAt : ValueObject
{
    private JoinedAt(
        DateTime value
    )
    {
        Value = value;
    }

    public DateTime Value { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static JoinedAt Create(
        DateTime value
    )
    {
        Validate(value);

        return new JoinedAt(value);
    }

    public static JoinedAt CreateSkipValidation(
        DateTime value
    )
    {
        return new JoinedAt(value);
    }

    public static JoinedAt CreateNow(
        IDateTimeProvider dateTimeProvider
    )
    {
        return Create(dateTimeProvider.UtcNow);
    }

    private static void Validate(
        DateTime value
    )
    {
        if (value.Kind != DateTimeKind.Utc) throw new AddedDateTimeInvalidKindException();
    }

    public static implicit operator DateTime(JoinedAt joinedAt)
    {
        return joinedAt.Value;
    }

    public static implicit operator JoinedAt(DateTime value)
    {
        return Create(value);
    }
}