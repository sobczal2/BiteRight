using BiteRight.Domain.Common;

namespace BiteRight.Domain.Categories;

public class Photo : Entity<PhotoId>
{
    public const string DefaultName = "default.webp";
    public const string ContentType = "image/webp";
    public const string Directory = "photos";

    // EF Core
    private Photo()
    {
        Name = default!;
    }

    private Photo(
        PhotoId id,
        string name
    )
        : base(id)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public static Photo Default { get; } = new(PhotoId.Empty, DefaultName);

    public static Photo Create(
        PhotoId id,
        string name
    )
    {
        return new Photo(id, name);
    }
}