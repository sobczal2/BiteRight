namespace BiteRight.Domain.Common;

public class Entity<TId> : IEquatable<Entity<TId>> where TId : GuidId
{
    public TId Id { get; }

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

    public override string ToString() => Id.ToString();
}