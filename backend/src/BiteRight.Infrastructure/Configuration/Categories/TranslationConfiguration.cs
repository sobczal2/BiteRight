// # ==============================================================================
// # Solution: BiteRight
// # File: TranslationConfiguration.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using System.Collections.Generic;
using BiteRight.Domain.Categories;
using BiteRight.Infrastructure.Configuration.Languages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Name = BiteRight.Domain.Categories.Name;

#endregion

namespace BiteRight.Infrastructure.Configuration.Categories;

public class TranslationConfiguration : IEntityTypeConfiguration<Translation>
{
    public static Translation OtherEn { get; } = Translation.Create(
        CategoryConfiguration.Other.Id,
        LanguageConfiguration.English.Id,
        Name.Create("Other"),
        new TranslationId(Guid.Parse("F7B5E10F-0719-4731-831A-FFE0A1A1ED07"))
    );

    public static Translation OtherPl { get; } = Translation.Create(
        CategoryConfiguration.Other.Id,
        LanguageConfiguration.Polish.Id,
        Name.Create("Inne"),
        new TranslationId(Guid.Parse("ABED62C9-41B4-462F-866B-06D714DEC958"))
    );

    public static Translation OtherDe { get; } = Translation.Create(
        CategoryConfiguration.Other.Id,
        LanguageConfiguration.German.Id,
        Name.Create("Andere"),
        new TranslationId(Guid.Parse("206A3C95-FB6D-4127-A37B-9F328C021021"))
    );

    public static Translation DairyEn { get; } = Translation.Create(
        CategoryConfiguration.Dairy.Id,
        LanguageConfiguration.English.Id,
        Name.Create("Dairy"),
        new TranslationId(Guid.Parse("38097234-329C-4372-A54F-13E6C41004FA"))
    );

    public static Translation DairyPl { get; } = Translation.Create(
        CategoryConfiguration.Dairy.Id,
        LanguageConfiguration.Polish.Id,
        Name.Create("Nabiał"),
        new TranslationId(Guid.Parse("96A2257A-F4C4-4C47-9B5B-50A7BE92C5DA"))
    );

    public static Translation DairyDe { get; } = Translation.Create(
        CategoryConfiguration.Dairy.Id,
        LanguageConfiguration.German.Id,
        Name.Create("Milchprodukte"),
        new TranslationId(Guid.Parse("C089F04F-FDFE-48E1-87B7-FF8028ECB67F"))
    );

    public static Translation FruitEn { get; } = Translation.Create(
        CategoryConfiguration.Fruit.Id,
        LanguageConfiguration.English.Id,
        Name.Create("Fruit"),
        new TranslationId(Guid.Parse("73127F14-9FE8-4DD7-B4BF-E11D98C6E505"))
    );

    public static Translation FruitPl { get; } = Translation.Create(
        CategoryConfiguration.Fruit.Id,
        LanguageConfiguration.Polish.Id,
        Name.Create("Owoce"),
        new TranslationId(Guid.Parse("AF5072C0-7282-481F-A623-F4E00FEB2BF8"))
    );

    public static Translation FruitDe { get; } = Translation.Create(
        CategoryConfiguration.Fruit.Id,
        LanguageConfiguration.German.Id,
        Name.Create("Obst"),
        new TranslationId(Guid.Parse("532BFDC2-BD1A-40EB-8723-65AE4E76F242"))
    );

    public static Translation VegetableEn { get; } = Translation.Create(
        CategoryConfiguration.Vegetable.Id,
        LanguageConfiguration.English.Id,
        Name.Create("Vegetable"),
        new TranslationId(Guid.Parse("7EC1928D-1517-4EF1-A24F-CF20322F34A5"))
    );

    public static Translation VegetablePl { get; } = Translation.Create(
        CategoryConfiguration.Vegetable.Id,
        LanguageConfiguration.Polish.Id,
        Name.Create("Warzywa"),
        new TranslationId(Guid.Parse("B9E41376-B606-42D3-B2AF-8373C1905B87"))
    );

    public static Translation VegetableDe { get; } = Translation.Create(
        CategoryConfiguration.Vegetable.Id,
        LanguageConfiguration.German.Id,
        Name.Create("Gemüse"),
        new TranslationId(Guid.Parse("115FBAA7-0BCC-4D41-B1F1-43FE1CDB28F9"))
    );

    public static Translation MeatEn { get; } = Translation.Create(
        CategoryConfiguration.Meat.Id,
        LanguageConfiguration.English.Id,
        Name.Create("Meat"),
        new TranslationId(Guid.Parse("A2C2D7CB-E684-4C71-80E0-07AA1DB6D5BA"))
    );

    public static Translation MeatPl { get; } = Translation.Create(
        CategoryConfiguration.Meat.Id,
        LanguageConfiguration.Polish.Id,
        Name.Create("Mięso"),
        new TranslationId(Guid.Parse("F7F1C6E7-012F-449C-95A3-79459F9331FB"))
    );

    public static Translation MeatDe { get; } = Translation.Create(
        CategoryConfiguration.Meat.Id,
        LanguageConfiguration.German.Id,
        Name.Create("Fleisch"),
        new TranslationId(Guid.Parse("04B42267-4C3E-4AC1-9972-90112BA7C952"))
    );

    public static Translation FishEn { get; } = Translation.Create(
        CategoryConfiguration.Fish.Id,
        LanguageConfiguration.English.Id,
        Name.Create("Fish"),
        new TranslationId(Guid.Parse("BD8FEFC6-70CE-4562-9730-274C589CA72B"))
    );

    public static Translation FishPl { get; } = Translation.Create(
        CategoryConfiguration.Fish.Id,
        LanguageConfiguration.Polish.Id,
        Name.Create("Ryby"),
        new TranslationId(Guid.Parse("15E9F981-3A22-4BB2-96F7-5CC559567794"))
    );

    public static Translation FishDe { get; } = Translation.Create(
        CategoryConfiguration.Fish.Id,
        LanguageConfiguration.German.Id,
        Name.Create("Fisch"),
        new TranslationId(Guid.Parse("C605CDF4-8B17-4CF0-950A-5A9BEE434145"))
    );

    public static Translation BeverageEn { get; } = Translation.Create(
        CategoryConfiguration.Beverage.Id,
        LanguageConfiguration.English.Id,
        Name.Create("Beverage"),
        new TranslationId(Guid.Parse("2E24CA76-C17C-4A80-874B-98A33B825567"))
    );

    public static Translation BeveragePl { get; } = Translation.Create(
        CategoryConfiguration.Beverage.Id,
        LanguageConfiguration.Polish.Id,
        Name.Create("Napój"),
        new TranslationId(Guid.Parse("329F0E4B-B364-41F9-AD75-804D796BCC74"))
    );

    public static Translation BeverageDe { get; } = Translation.Create(
        CategoryConfiguration.Beverage.Id,
        LanguageConfiguration.German.Id,
        Name.Create("Getränk"),
        new TranslationId(Guid.Parse("62334E0C-ACBA-47C7-A98A-9A606FED9084"))
    );

    public static Translation SnackEn { get; } = Translation.Create(
        CategoryConfiguration.Snack.Id,
        LanguageConfiguration.English.Id,
        Name.Create("Snack"),
        new TranslationId(Guid.Parse("718BADD8-B2C2-4D4D-BD2B-2959F4949080"))
    );

    public static Translation SnackPl { get; } = Translation.Create(
        CategoryConfiguration.Snack.Id,
        LanguageConfiguration.Polish.Id,
        Name.Create("Przekąska"),
        new TranslationId(Guid.Parse("4DAA9144-5F43-401B-8622-3AC7B1CC14FF"))
    );

    public static Translation SnackDe { get; } = Translation.Create(
        CategoryConfiguration.Snack.Id,
        LanguageConfiguration.German.Id,
        Name.Create("Snack"),
        new TranslationId(Guid.Parse("B151FA8F-6361-40E5-99DA-1AD19108AF04"))
    );

    public static Translation WheatEn { get; } = Translation.Create(
        CategoryConfiguration.Wheat.Id,
        LanguageConfiguration.English.Id,
        Name.Create("Wheat"),
        new TranslationId(Guid.Parse("F121FB7F-CE98-4348-9E1D-9353C1D82DF9"))
    );

    public static Translation WheatPl { get; } = Translation.Create(
        CategoryConfiguration.Wheat.Id,
        LanguageConfiguration.Polish.Id,
        Name.Create("Pszenica"),
        new TranslationId(Guid.Parse("432866DF-EF78-4EAE-8CC3-0E2AB2EFB801"))
    );

    public static Translation WheatDe { get; } = Translation.Create(
        CategoryConfiguration.Wheat.Id,
        LanguageConfiguration.German.Id,
        Name.Create("Weizen"),
        new TranslationId(Guid.Parse("24BA2BC6-8BC6-4A11-96DC-0DE686DE742F"))
    );
    
    public static Translation DishEn { get; } = Translation.Create(
        CategoryConfiguration.Dish.Id,
        LanguageConfiguration.English.Id,
        Name.Create("Dish"),
        new TranslationId(Guid.Parse("BFB53DFB-CB0F-4BA4-9EC6-4052B4E3AE98"))
    );
    
    public static Translation DishPl { get; } = Translation.Create(
        CategoryConfiguration.Dish.Id,
        LanguageConfiguration.Polish.Id,
        Name.Create("Danie"),
        new TranslationId(Guid.Parse("3C2F5D1E-16F6-4109-B42F-EC4F1731EA62"))
    );
    
    public static Translation DishDe { get; } = Translation.Create(
        CategoryConfiguration.Dish.Id,
        LanguageConfiguration.German.Id,
        Name.Create("Gericht"),
        new TranslationId(Guid.Parse("6729644A-7232-4483-BD90-7839732F6C7B"))
    );

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
        yield return OtherEn;
        yield return OtherPl;
        yield return OtherDe;
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
        yield return BeverageEn;
        yield return BeveragePl;
        yield return BeverageDe;
        yield return SnackEn;
        yield return SnackPl;
        yield return SnackDe;
        yield return WheatEn;
        yield return WheatPl;
        yield return WheatDe;
        yield return DishEn;
        yield return DishPl;
        yield return DishDe;
    }
}