using BiteRight.Domain.Common;

namespace BiteRight.Domain.Products;

public class ProductId : GuidId
{
    public ProductId()
    {
    }

    public ProductId(
        Guid value
    )
        : base(value)
    {
    }

    public static implicit operator Guid(
        ProductId id
    )
    {
        return id.Value;
    }

    public static implicit operator ProductId(
        Guid id
    )
    {
        return new ProductId(id);
    }
}