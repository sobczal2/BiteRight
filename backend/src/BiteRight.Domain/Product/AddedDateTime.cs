using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Common;
using BiteRight.Domain.Product.Exceptions;

namespace BiteRight.Domain.Product;

public class AddedDateTime : ValueObject
{
    public DateTime Value { get; }

    private AddedDateTime(
        DateTime value
    )
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static AddedDateTime Create(
        DateTime value
    )
    {
        Validate(value);

        return new AddedDateTime(value);
    }

    public static AddedDateTime CreateSkipValidation(
        DateTime value
    )
    {
        return new AddedDateTime(value);
    }

    public static AddedDateTime CreateNow(
        IDateTimeProvider dateTimeProvider
    )
    {
        return Create(dateTimeProvider.UtcNow);
    }

    private static void Validate(
        DateTime value
    )
    {
        if (value.Kind != DateTimeKind.Utc)
        {
            throw new AddedDateTimeInvalidKindException();
        }
    }
}