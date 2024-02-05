namespace BiteRight.Domain.Common;

public class Id<T>
    where T : notnull
{
    public T Value { get; }
    
    public Id(
        T value
    )
    {
        Value = value;
    }
    
    public override bool Equals(
        object? obj
    )
    {
        if (obj is not Id<T> other)
        {
            return false;
        }
        
        return Value.Equals(other.Value);
    }
    
    public override int GetHashCode() => Value.GetHashCode();
    
    public override string ToString() => Value.ToString() ?? string.Empty;
    
    public static implicit operator T(
        Id<T> self
    ) => self.Value;
    
    public static implicit operator Id<T>(
        T value
    ) => new(value);
}