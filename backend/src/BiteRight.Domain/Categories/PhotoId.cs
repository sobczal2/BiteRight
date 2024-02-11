using BiteRight.Domain.Common;

namespace BiteRight.Domain.Categories;

public class PhotoId : GuidId
{
    public static PhotoId Empty => new(Guid.Empty);

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
    {
        return new PhotoId(id);
    }

    public static implicit operator Guid(PhotoId id)
    {
        return id.Value;
    }
}