using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Common;
using BiteRight.Domain.Languages;

namespace BiteRight.Domain.Categories;

public class Category : AggregateRoot<CategoryId>
{
    public PhotoId? PhotoId { get; private set; }
    public virtual Photo? Photo { get; }
    public IEnumerable<Translation> Translations { get; }

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
        Translations = new List<Translation>();
    }

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