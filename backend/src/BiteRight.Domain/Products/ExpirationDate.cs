using BiteRight.Domain.Abstracts.Common;
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

    public static ExpirationDate CreateUnknown()
    {
        return Create(DateOnly.MaxValue, ExpirationDateKind.Unknown);
    }

    public static ExpirationDate CreateInfinite()
    {
        return Create(DateOnly.MaxValue, ExpirationDateKind.Infinite);
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
        if (kind == ExpirationDateKind.Infinite && value != DateOnly.MaxValue)
        {
            throw new ExpirationDateInfiniteValueException();
        }
    }

    public enum ExpirationDateKind
    {
        Unknown = 0,
        Infinite = 1,
        BestBefore = 2,
        UseBy = 3
    }

    public DateOnly? GetDateIfKnown()
    {
        return Kind switch
        {
            ExpirationDateKind.Unknown => null,
            ExpirationDateKind.Infinite => null,
            _ => Value
        };
    }
}