using BiteRight.Domain.Common;

namespace BiteRight.Domain.Product;

public class PriceId : GuidId
{
    public PriceId()
    {
    }

    public PriceId(
        Guid value
    )
        : base(value)
    {
    }

    public static implicit operator Guid(
        PriceId id
    ) => id.Value;

    public static implicit operator PriceId(
        Guid id
    ) => new(id);
}