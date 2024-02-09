using BiteRight.Domain.Common;
using BiteRight.Domain.Products.Exceptions;

namespace BiteRight.Domain.Products;

public class DisposedState : ValueObject
{
    public bool Disposed { get; private set; }
    public DateTime? DisposedDate { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Disposed;
        yield return DisposedDate!;
    }

    // EF Core
    private DisposedState()
    {
        Disposed = default!;
        DisposedDate = default!;
    }

    private DisposedState(
        bool disposed,
        DateTime? disposedDate
    )
    {
        Disposed = disposed;
        DisposedDate = disposedDate;
    }

    private static DisposedState Create(
        bool disposed,
        DateTime? disposedDate
    )
    {
        Validate(disposed, disposedDate);
        return new DisposedState(disposed, disposedDate);
    }

    private static void Validate(
        bool disposed,
        DateTime? disposedDate
    )
    {
        if (!disposed)
        {
            if (disposedDate.HasValue)
            {
                throw new DisposedStateInvalidDisposedDateValueException();
            }
        }
        else
        {
            if (disposedDate is not { Kind: DateTimeKind.Utc })
            {
                throw new DisposedStateInvalidDisposedDateValueException();
            }
        }
    }

    public static DisposedState CreateNotDisposed()
    {
        return Create(false, null);
    }

    public static DisposedState CreateDisposed(
        DateTime disposedDate
    )
    {
        return Create(true, disposedDate);
    }
}