using BiteRight.Domain.Common;
using BiteRight.Domain.Languages;

namespace BiteRight.Domain.Categories;

public class Translation : Entity<TranslationId>
{
    public CategoryId CategoryId { get; private set; }
    public virtual Category Category { get; private set; }
    public LanguageId LanguageId { get; private set; }
    public virtual Language Language { get; private set; }
    public Name Name { get; private set; }
    
    // EF Core
    private Translation()
    {
        CategoryId = default!;
        Category = default!;
        LanguageId = default!;
        Language = default!;
        Name = default!;
    }
    
    private Translation(
        TranslationId id,
        CategoryId categoryId,
        LanguageId languageId,
        Name name
    )
        : base(id)
    {
        CategoryId = categoryId;
        Category = default!;
        LanguageId = languageId;
        Language = default!;
        Name = name;
    }
    
    public static Translation Create(
        CategoryId categoryId,
        LanguageId languageId,
        Name name,
        TranslationId? id = null
    )
        => new(
            id ?? new TranslationId(),
            categoryId,
            languageId,
            name
        );
}