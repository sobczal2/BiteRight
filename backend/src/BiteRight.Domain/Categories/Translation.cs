using BiteRight.Domain.Common;
using BiteRight.Domain.Languages;

namespace BiteRight.Domain.Categories;

public class Translation : Entity<TranslationId>
{
    public CategoryId CategoryId { get; private set; }
    public Category Category { get; private set; } = default!;
    public LanguageId LanguageId { get; private set; }
    public Language Language { get; private set; } = default!;
    public Name Name { get; private set; }
    
    // EF Core
    private Translation()
    {
        CategoryId = default!;
        Name = default!;
        LanguageId = default!;
    }
    
    private Translation(
        TranslationId id,
        CategoryId categoryId,
        Name name,
        LanguageId languageId
    )
        : base(id)
    {
        CategoryId = categoryId;
        Name = name;
        LanguageId = languageId;
    }
    
    public static Translation Create(
        CategoryId categoryId,
        Name name,
        LanguageId languageId,
        TranslationId? id = null
    )
        => new(
            id ?? new TranslationId(),
            categoryId,
            name,
            languageId
        );
}