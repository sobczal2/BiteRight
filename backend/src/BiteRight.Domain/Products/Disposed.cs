using BiteRight.Domain.Common;

namespace BiteRight.Domain.Products;

public class Disposed : ValueObject
{
    public  static readonly Disposed NotDisposed = false;
    public  static readonly Disposed WasDisposed = true;
    public bool Value { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private Disposed(
        bool value
    )
    {
        Value = value;
    }

    public static Disposed Create(
        bool value
    )
    {
        return new Disposed(value);
    }

    public static Disposed CreateNotDisposed()
    {
        return Create(false);
    }

    public static implicit operator bool(
        Disposed disposed
    )
    {
        return disposed.Value;
    }

    public static implicit operator Disposed(
        bool value
    )
    {
        return Create(value);
    }
}