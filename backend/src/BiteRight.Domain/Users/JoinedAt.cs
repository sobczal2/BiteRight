using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Common;
using BiteRight.Domain.Products.Exceptions;

namespace BiteRight.Domain.Users;

public class JoinedAt : ValueObject
{
    public DateTime Value { get; }

    private JoinedAt(
        DateTime value
    )
    {
        Value = value;
    }

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