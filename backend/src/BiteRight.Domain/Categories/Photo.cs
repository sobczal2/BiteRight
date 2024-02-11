using System;
using BiteRight.Domain.Common;

namespace BiteRight.Domain.Categories;

public class Photo : Entity<PhotoId>
{
    public const string DefaultName = "default.webp";
    public const string ContentType = "image/webp";
    public const string Directory = "photos";
    public static readonly PhotoId DefaultId = new(Guid.Parse("4AA9576B-8F3D-4A09-A66E-CAA3FDDFB4FB"));

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

    public static Photo Default { get; } = new(DefaultId, DefaultName);

    public static Photo Create(
        PhotoId id,
        string name
    )
    {
        return new Photo(id, name);
    }
}