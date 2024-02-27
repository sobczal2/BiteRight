// # ==============================================================================
// # Solution: BiteRight
// # File: PhotoId.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Domain.Common;

#endregion

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

    public static PhotoId Empty => new(Guid.Empty);

    public static implicit operator PhotoId(
        Guid id
    )
    {
        return new PhotoId(id);
    }

    public static implicit operator Guid(
        PhotoId id
    )
    {
        return id.Value;
    }
}