namespace BiteRight.Domain.Common;

public abstract class Id<T>
    where T : notnull
{
    public Id(
        T value
    )
    {
        Value = value;
    }

    public T Value { get; }

    public override bool Equals(object? obj)
    {
        if (obj is null || GetType() != obj.GetType()) return false;

        var id = (Id<T>)obj;
        return EqualityComparer<T>.Default.Equals(Value, id.Value);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value.ToString() ?? string.Empty;
    }
}