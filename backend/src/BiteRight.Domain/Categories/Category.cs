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
        Photo? photo
    )
        : base(id)
    {
        Photo = photo;
        PhotoId = photo?.Id;
        Translations = new List<Translation>();
    }

    public static Category Create(
        Photo? photo,
        IDomainEventFactory? domainEventFactory = null,
        CategoryId? id = null
    )
    {
        var category = new Category(
            id ?? new CategoryId(),
            photo
        );

        if (domainEventFactory is not null)
        {
            category.AddDomainEvent(
                domainEventFactory.CreateCategoryCreatedEvent(
                    category.Id
                )
            );
        }

        return category;
    }
    public Uri GetPhotoUri()
    {
        return Photo?.GetUri() ?? Photo.Default.GetUri();
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