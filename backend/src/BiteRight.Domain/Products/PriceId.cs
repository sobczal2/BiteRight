using BiteRight.Domain.Common;

namespace BiteRight.Domain.Products;

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
    )
    {
        return id.Value;
    }

    public static implicit operator PriceId(
        Guid id
    )
    {
        return new PriceId(id);
    }
}