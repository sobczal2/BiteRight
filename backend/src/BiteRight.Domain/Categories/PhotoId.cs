using BiteRight.Domain.Common;

namespace BiteRight.Domain.Categories;

public class PhotoId : GuidId
{
    public PhotoId()
    {
    }

    public PhotoId(
        Guid value
    )
        : base(value)
    {
    }
    
    public static implicit operator PhotoId(Guid id)
        => new(id);
    public static implicit operator Guid(PhotoId id)
        => id.Value;
    
    public static PhotoId Empty => new(Guid.Empty);
}