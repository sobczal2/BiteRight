using BiteRight.Domain.Categories;
using BiteRight.Infrastructure.Configuration.Languages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Name = BiteRight.Domain.Categories.Name;

namespace BiteRight.Infrastructure.Configuration.Categories;

public class TranslationConfiguration : IEntityTypeConfiguration<Translation>
{
    public void Configure(
        EntityTypeBuilder<Translation> builder
    )
    {
        builder.ToTable("category_translations", "category");
        builder.HasKey(categoryTranslation => categoryTranslation.Id);
        builder.Property(categoryTranslation => categoryTranslation.Id)
            .HasConversion(
                id => id.Value,
                value => value
            )
            .ValueGeneratedNever();
        builder.Property(categoryTranslation => categoryTranslation.CategoryId)
            .HasConversion(
                categoryId => categoryId.Value,
                value => value
            );
        builder.Property(categoryTranslation => categoryTranslation.LanguageId)
            .HasConversion(
                languageId => languageId.Value,
                value => value
            );
        builder.Property(categoryTranslation => categoryTranslation.Name)
            .HasConversion(
                name => name.Value,
                value => Name.CreateSkipValidation(value)
            );
        builder.HasOne(ct => ct.Category)
            .WithMany(c => c.Translations)
            .HasForeignKey(ct => ct.CategoryId);
        builder.HasOne(ct => ct.Language)
            .WithMany()
            .HasForeignKey(ct => ct.LanguageId);

        builder.HasData(GetSeedData());
    }

    private static IEnumerable<Translation> GetSeedData()
    {
        yield return DairyEn;
        yield return DairyPl;
        yield return DairyDe;
        yield return FruitEn;
        yield return FruitPl;
        yield return FruitDe;
        yield return VegetableEn;
        yield return VegetablePl;
        yield return VegetableDe;
        yield return MeatEn;
        yield return MeatPl;
        yield return MeatDe;
        yield return FishEn;
        yield return FishPl;
        yield return FishDe;
        yield return NoneEn;
        yield return NonePl;
        yield return NoneDe;
    }

    public static Translation DairyEn { get; } = Translation.Create(
        CategoryConfiguration.Dairy.Id,
        Name.Create("Dairy"),
        LanguageConfiguration.English.Id,
        new TranslationId(new Guid("38097234-329C-4372-A54F-13E6C41004FA"))
    );

    public static Translation DairyPl { get; } = Translation.Create(
        CategoryConfiguration.Dairy.Id,
        Name.Create("Nabiał"),
        LanguageConfiguration.Polish.Id,
        new TranslationId(new Guid("96A2257A-F4C4-4C47-9B5B-50A7BE92C5DA"))
    );

    public static Translation DairyDe { get; } = Translation.Create(
        CategoryConfiguration.Dairy.Id,
        Name.Create("Milchprodukte"),
        LanguageConfiguration.German.Id,
        new TranslationId(new Guid("C089F04F-FDFE-48E1-87B7-FF8028ECB67F"))
    );

    public static Translation FruitEn { get; } = Translation.Create(
        CategoryConfiguration.Fruit.Id,
        Name.Create("Fruit"),
        LanguageConfiguration.English.Id,
        new TranslationId(new Guid("73127F14-9FE8-4DD7-B4BF-E11D98C6E505"))
    );

    public static Translation FruitPl { get; } = Translation.Create(
        CategoryConfiguration.Fruit.Id,
        Name.Create("Owoce"),
        LanguageConfiguration.Polish.Id,
        new TranslationId(new Guid("AF5072C0-7282-481F-A623-F4E00FEB2BF8"))
    );

    public static Translation FruitDe { get; } = Translation.Create(
        CategoryConfiguration.Fruit.Id,
        Name.Create("Obst"),
        LanguageConfiguration.German.Id,
        new TranslationId(new Guid("532BFDC2-BD1A-40EB-8723-65AE4E76F242"))
    );

    public static Translation VegetableEn { get; } = Translation.Create(
        CategoryConfiguration.Vegetable.Id,
        Name.Create("Vegetable"),
        LanguageConfiguration.English.Id,
        new TranslationId(new Guid("7EC1928D-1517-4EF1-A24F-CF20322F34A5"))
    );

    public static Translation VegetablePl { get; } = Translation.Create(
        CategoryConfiguration.Vegetable.Id,
        Name.Create("Warzywa"),
        LanguageConfiguration.Polish.Id,
        new TranslationId(new Guid("B9E41376-B606-42D3-B2AF-8373C1905B87"))
    );

    public static Translation VegetableDe { get; } = Translation.Create(
        CategoryConfiguration.Vegetable.Id,
        Name.Create("Gemüse"),
        LanguageConfiguration.German.Id,
        new TranslationId(new Guid("115FBAA7-0BCC-4D41-B1F1-43FE1CDB28F9"))
    );

    public static Translation MeatEn { get; } = Translation.Create(
        CategoryConfiguration.Meat.Id,
        Name.Create("Meat"),
        LanguageConfiguration.English.Id,
        new TranslationId(new Guid("A2C2D7CB-E684-4C71-80E0-07AA1DB6D5BA"))
    );

    public static Translation MeatPl { get; } = Translation.Create(
        CategoryConfiguration.Meat.Id,
        Name.Create("Mięso"),
        LanguageConfiguration.Polish.Id,
        new TranslationId(new Guid("F7F1C6E7-012F-449C-95A3-79459F9331FB"))
    );

    public static Translation MeatDe { get; } = Translation.Create(
        CategoryConfiguration.Meat.Id,
        Name.Create("Fleisch"),
        LanguageConfiguration.German.Id,
        new TranslationId(new Guid("04B42267-4C3E-4AC1-9972-90112BA7C952"))
    );

    public static Translation FishEn { get; } = Translation.Create(
        CategoryConfiguration.Fish.Id,
        Name.Create("Fish"),
        LanguageConfiguration.English.Id,
        new TranslationId(new Guid("BD8FEFC6-70CE-4562-9730-274C589CA72B"))
    );

    public static Translation FishPl { get; } = Translation.Create(
        CategoryConfiguration.Fish.Id,
        Name.Create("Ryby"),
        LanguageConfiguration.Polish.Id,
        new TranslationId(new Guid("15E9F981-3A22-4BB2-96F7-5CC559567794"))
    );

    public static Translation FishDe { get; } = Translation.Create(
        CategoryConfiguration.Fish.Id,
        Name.Create("Fisch"),
        LanguageConfiguration.German.Id,
        new TranslationId(new Guid("C605CDF4-8B17-4CF0-950A-5A9BEE434145"))
    );

    public static Translation NoneEn { get; } = Translation.Create(
        CategoryConfiguration.None.Id,
        Name.Create("None"),
        LanguageConfiguration.English.Id,
        new TranslationId(new Guid("F7B5E10F-0719-4731-831A-FFE0A1A1ED07"))
    );

    public static Translation NonePl { get; } = Translation.Create(
        CategoryConfiguration.None.Id,
        Name.Create("Brak"),
        LanguageConfiguration.Polish.Id,
        new TranslationId(new Guid("ABED62C9-41B4-462F-866B-06D714DEC958"))
    );

    public static Translation NoneDe { get; } = Translation.Create(
        CategoryConfiguration.None.Id,
        Name.Create("Keine"),
        LanguageConfiguration.German.Id,
        new TranslationId(new Guid("206A3C95-FB6D-4127-A37B-9F328C021021"))
    );
}