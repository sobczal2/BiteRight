using BiteRight.Domain.Common;

namespace BiteRight.Domain.Categories;

public class Photo : Entity<PhotoId>
{
    private Photo(
        PhotoId id
    )
        : base(id)
    {
    }

    public static Photo Create(
        PhotoId id
    )
        => new(id);

    public static Photo Default { get; } = new(PhotoId.Empty);

    public Uri GetUri()
    {
        return new Uri($"photos/{Id}.webp");
    }
}