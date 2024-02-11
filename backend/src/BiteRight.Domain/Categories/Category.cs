using System;
using System.Collections.Generic;
using System.Linq;
using BiteRight.Domain.Common;
using BiteRight.Domain.Languages;

namespace BiteRight.Domain.Categories;

public class Category : AggregateRoot<CategoryId>
{
    // EF Core
    private Category()
    {
        PhotoId = default!;
        Photo = default!;
        Translations = default!;
    }

    private Category(
        CategoryId id,
        PhotoId? photoId
    )
        : base(id)
    {
        PhotoId = photoId;
        Photo = default!;
        Translations = default!;
    }

    public PhotoId? PhotoId { get; private set; }
    public virtual Photo? Photo { get; }
    public IEnumerable<Translation> Translations { get; }

    public static Category Create(
        PhotoId? photoId,
        CategoryId? id = null
    )
    {
        var category = new Category(
            id ?? new CategoryId(),
            photoId
        );


        return category;
    }

    public string GetPhotoName()
    {
        return Photo?.Name ?? Photo.DefaultName;
    }

    public string GetName(
        LanguageId languageId
    )
    {
        return Translations
            .SingleOrDefault(t => Equals(t.LanguageId, languageId))
            ?.Name ?? throw new InvalidOperationException();
    }
}