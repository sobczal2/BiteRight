using BiteRight.Domain.Common;
using BiteRight.Domain.Products.Exceptions;

namespace BiteRight.Domain.Products;

public class ExpirationDate : ValueObject
{
    public DateOnly Value { get; }
    public ExpirationDateKind Kind { get; }
    
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

    public static ExpirationDate CreateUnknown(
        DateOnly? value = null
    )
    {
        if (value == null)
            value = DateOnly.MinValue;
        return Create(value.Value, ExpirationDateKind.Unknown);
    }

    public static ExpirationDate CreateInfinite()
    {
        return Create(DateOnly.MinValue, ExpirationDateKind.Infinite);
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
        if (kind == ExpirationDateKind.Infinite && value != DateOnly.MinValue)
        {
            throw new ExpirationDateInfiniteValueException();
        }
    }

    public bool IsInfinite() => Value == DateOnly.MaxValue;

    public enum ExpirationDateKind
    {
        Unknown = 0,
        Infinite = 1,
        BestBefore = 2,
        UseBy = 3
    }
}