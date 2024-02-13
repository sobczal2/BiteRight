// # ==============================================================================
// # Solution: BiteRight
// # File: Entity.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using System.Collections.Generic;

#endregion

namespace BiteRight.Domain.Common;

public class Entity<TId> : IEquatable<Entity<TId>> where TId : GuidId
{
    protected Entity()
    {
        Id = default!;
    }

    public Entity(
        TId id
    )
    {
        Id = id;
    }

    public TId Id { get; protected set; }

    public bool Equals(
        Entity<TId>? other
    )
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override bool Equals(
        object? obj
    )
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Entity<TId>)obj);
    }

    public static bool operator ==(
        Entity<TId>? left,
        Entity<TId>? right
    )
    {
        return Equals(left, right);
    }

    public static bool operator !=(
        Entity<TId>? left,
        Entity<TId>? right
    )
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<TId>.Default.GetHashCode(Id);
    }

    public override string ToString()
    {
        return Id.ToString();
    }
}