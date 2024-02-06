using BiteRight.Domain.Common;

namespace BiteRight.Domain.Categories;

public class Photo : Entity<PhotoId>
{
    public string Name { get; private set; }
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

    public static Photo Create(
        PhotoId id,
        string name
    )
        => new(id, name);

    public static Photo Default { get; } = new(PhotoId.Empty, DefaultName);
}