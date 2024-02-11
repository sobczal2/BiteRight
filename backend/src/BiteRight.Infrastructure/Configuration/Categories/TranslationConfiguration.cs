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
        builder.HasKey(translation => translation.Id);
        builder.Property(translation => translation.Id)
            .HasConversion(
                id => id.Value,
                value => value
            )
            .ValueGeneratedNever();

        builder.Property(translation => translation.CategoryId)
            .HasConversion(
                categoryId => categoryId.Value,
                value => value
            );

        builder.Property(translation => translation.LanguageId)
            .HasConversion(
                languageId => languageId.Value,
                value => value
            );

        builder.Property(translation => translation.Name)
            .HasConversion(
                name => name.Value,
                value => Name.CreateSkipValidation(value)
            );

        builder.HasOne(translation => translation.Category)
            .WithMany(category => category.Translations)
            .HasForeignKey(translation => translation.CategoryId);

        builder.HasOne(translation => translation.Language)
            .WithMany()
            .HasForeignKey(translation => translation.LanguageId);

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
        LanguageConfiguration.English.Id,
        Name.Create("Dairy"),
        new TranslationId(new Guid("38097234-329C-4372-A54F-13E6C41004FA"))
    );

    public static Translation DairyPl { get; } = Translation.Create(
        CategoryConfiguration.Dairy.Id,
        LanguageConfiguration.Polish.Id,
        Name.Create("Nabiał"),
        new TranslationId(new Guid("96A2257A-F4C4-4C47-9B5B-50A7BE92C5DA"))
    );

    public static Translation DairyDe { get; } = Translation.Create(
        CategoryConfiguration.Dairy.Id,
        LanguageConfiguration.German.Id,
        Name.Create("Milchprodukte"),
        new TranslationId(new Guid("C089F04F-FDFE-48E1-87B7-FF8028ECB67F"))
    );

    public static Translation FruitEn { get; } = Translation.Create(
        CategoryConfiguration.Fruit.Id,
        LanguageConfiguration.English.Id,
        Name.Create("Fruit"),
        new TranslationId(new Guid("73127F14-9FE8-4DD7-B4BF-E11D98C6E505"))
    );

    public static Translation FruitPl { get; } = Translation.Create(
        CategoryConfiguration.Fruit.Id,
        LanguageConfiguration.Polish.Id,
        Name.Create("Owoce"),
        new TranslationId(new Guid("AF5072C0-7282-481F-A623-F4E00FEB2BF8"))
    );

    public static Translation FruitDe { get; } = Translation.Create(
        CategoryConfiguration.Fruit.Id,
        LanguageConfiguration.German.Id,
        Name.Create("Obst"),
        new TranslationId(new Guid("532BFDC2-BD1A-40EB-8723-65AE4E76F242"))
    );

    public static Translation VegetableEn { get; } = Translation.Create(
        CategoryConfiguration.Vegetable.Id,
        LanguageConfiguration.English.Id,
        Name.Create("Vegetable"),
        new TranslationId(new Guid("7EC1928D-1517-4EF1-A24F-CF20322F34A5"))
    );

    public static Translation VegetablePl { get; } = Translation.Create(
        CategoryConfiguration.Vegetable.Id,
        LanguageConfiguration.Polish.Id,
        Name.Create("Warzywa"),
        new TranslationId(new Guid("B9E41376-B606-42D3-B2AF-8373C1905B87"))
    );

    public static Translation VegetableDe { get; } = Translation.Create(
        CategoryConfiguration.Vegetable.Id,
        LanguageConfiguration.German.Id,
        Name.Create("Gemüse"),
        new TranslationId(new Guid("115FBAA7-0BCC-4D41-B1F1-43FE1CDB28F9"))
    );

    public static Translation MeatEn { get; } = Translation.Create(
        CategoryConfiguration.Meat.Id,
        LanguageConfiguration.English.Id,
        Name.Create("Meat"),
        new TranslationId(new Guid("A2C2D7CB-E684-4C71-80E0-07AA1DB6D5BA"))
    );

    public static Translation MeatPl { get; } = Translation.Create(
        CategoryConfiguration.Meat.Id,
        LanguageConfiguration.Polish.Id,
        Name.Create("Mięso"),
        new TranslationId(new Guid("F7F1C6E7-012F-449C-95A3-79459F9331FB"))
    );

    public static Translation MeatDe { get; } = Translation.Create(
        CategoryConfiguration.Meat.Id,
        LanguageConfiguration.German.Id,
        Name.Create("Fleisch"),
        new TranslationId(new Guid("04B42267-4C3E-4AC1-9972-90112BA7C952"))
    );

    public static Translation FishEn { get; } = Translation.Create(
        CategoryConfiguration.Fish.Id,
        LanguageConfiguration.English.Id,
        Name.Create("Fish"),
        new TranslationId(new Guid("BD8FEFC6-70CE-4562-9730-274C589CA72B"))
    );

    public static Translation FishPl { get; } = Translation.Create(
        CategoryConfiguration.Fish.Id,
        LanguageConfiguration.Polish.Id,
        Name.Create("Ryby"),
        new TranslationId(new Guid("15E9F981-3A22-4BB2-96F7-5CC559567794"))
    );

    public static Translation FishDe { get; } = Translation.Create(
        CategoryConfiguration.Fish.Id,
        LanguageConfiguration.German.Id,
        Name.Create("Fisch"),
        new TranslationId(new Guid("C605CDF4-8B17-4CF0-950A-5A9BEE434145"))
    );

    public static Translation NoneEn { get; } = Translation.Create(
        CategoryConfiguration.None.Id,
        LanguageConfiguration.English.Id,
        Name.Create("None"),
        new TranslationId(new Guid("F7B5E10F-0719-4731-831A-FFE0A1A1ED07"))
    );

    public static Translation NonePl { get; } = Translation.Create(
        CategoryConfiguration.None.Id,
        LanguageConfiguration.Polish.Id,
        Name.Create("Brak"),
        new TranslationId(new Guid("ABED62C9-41B4-462F-866B-06D714DEC958"))
    );

    public static Translation NoneDe { get; } = Translation.Create(
        CategoryConfiguration.None.Id,
        LanguageConfiguration.German.Id,
        Name.Create("Keine"),
        new TranslationId(new Guid("206A3C95-FB6D-4127-A37B-9F328C021021"))
    );
}