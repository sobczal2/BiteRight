using BiteRight.Domain.Common;
using BiteRight.Domain.Products.Exceptions;

namespace BiteRight.Domain.Products;

public class DisposedState : ValueObject
{
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

    public bool Disposed { get; }
    public DateTime? DisposedDate { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Disposed;
        yield return DisposedDate!;
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
            if (disposedDate.HasValue) throw new DisposedStateInvalidDisposedDateValueException();
        }
        else
        {
            if (disposedDate is not { Kind: DateTimeKind.Utc })
                throw new DisposedStateInvalidDisposedDateValueException();
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