using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Common;
using BiteRight.Domain.Languages;

namespace BiteRight.Domain.Categories;

public class Category : AggregateRoot<CategoryId>
{
    public Photo? Photo { get; }
    public IEnumerable<CategoryTranslation> Translations { get; }

    // EF Core
    private Category()
    {
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
        Translations = new List<CategoryTranslation>();
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