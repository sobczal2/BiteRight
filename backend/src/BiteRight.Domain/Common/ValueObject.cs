// # ==============================================================================
// # Solution: BiteRight
// # File: ValueObject.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace BiteRight.Domain.Common;

public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(
        object? obj
    )
    {
        var valueObject = obj as ValueObject;

        if (ReferenceEquals(valueObject, null))
            return false;

        if (GetType() != obj?.GetType())
            return false;

        return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(1, (
                current,
                obj
            ) =>
            {
                unchecked
                {
                    return current * 23 + (obj?.GetHashCode() ?? 0);
                }
            });
    }

    public static bool operator ==(
        ValueObject a,
        ValueObject b
    )
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(
        ValueObject a,
        ValueObject b
    )
    {
        return !(a == b);
    }
}