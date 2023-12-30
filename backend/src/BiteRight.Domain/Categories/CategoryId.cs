using BiteRight.Domain.Common;

namespace BiteRight.Domain.Categories;

public class CategoryId : GuidId
{
    public CategoryId()
    {
    }

    public CategoryId(
        Guid value
    )
        : base(value)
    {
    }
    
    public static implicit operator CategoryId(Guid id)
        => new(id);
    public static implicit operator Guid(CategoryId id)
        => id.Value;
}