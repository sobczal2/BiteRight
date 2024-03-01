// # ==============================================================================
// # Solution: BiteRight
// # File: TranslationConfiguration.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using System.Collections.Generic;
using BiteRight.Domain.Units;
using BiteRight.Infrastructure.Configuration.Languages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Name = BiteRight.Domain.Units.Name;

#endregion

namespace BiteRight.Infrastructure.Configuration.Units;

public class TranslationConfiguration : IEntityTypeConfiguration<Translation>
{
    public static Translation LiterEn { get; } = Translation.Create(
        UnitConfiguration.Liter.Id,
        LanguageConfiguration.English.Id,
        Name.CreateSkipValidation("Liter"),
        Abbreviation.CreateSkipValidation("L"),
        new TranslationId(Guid.Parse("D91F5A31-458E-4D08-BFEE-1812183EA35D"))
    );

    public static Translation LiterPl { get; } = Translation.Create(
        UnitConfiguration.Liter.Id,
        LanguageConfiguration.Polish.Id,
        Name.CreateSkipValidation("Litr"),
        Abbreviation.CreateSkipValidation("L"),
        new TranslationId(Guid.Parse("CA5C1BAC-A53D-44BF-9CDF-2CD165047F7B"))
    );

    public static Translation LiterDe { get; } = Translation.Create(
        UnitConfiguration.Liter.Id,
        LanguageConfiguration.German.Id,
        Name.CreateSkipValidation("Liter"),
        Abbreviation.CreateSkipValidation("L"),
        new TranslationId(Guid.Parse("CE4F6636-140C-48F7-A2F4-3148045CA0E5"))
    );

    public static Translation KilogramEn { get; } = Translation.Create(
        UnitConfiguration.Kilogram.Id,
        LanguageConfiguration.English.Id,
        Name.CreateSkipValidation("Kilogram"),
        Abbreviation.CreateSkipValidation("kg"),
        new TranslationId(Guid.Parse("712CD66C-87AC-4AF4-BC07-2D42DB04E8B3"))
    );

    public static Translation KilogramPl { get; } = Translation.Create(
        UnitConfiguration.Kilogram.Id,
        LanguageConfiguration.Polish.Id,
        Name.CreateSkipValidation("Kilogram"),
        Abbreviation.CreateSkipValidation("kg"),
        new TranslationId(Guid.Parse("E45767EE-C69C-4C34-9729-FFA396306017"))
    );

    public static Translation KilogramDe { get; } = Translation.Create(
        UnitConfiguration.Kilogram.Id,
        LanguageConfiguration.German.Id,
        Name.CreateSkipValidation("Kilogramm"),
        Abbreviation.CreateSkipValidation("kg"),
        new TranslationId(Guid.Parse("368FB52E-06D7-4B33-ACEB-C7EDFDF865AD"))
    );
    
    public static Translation GramEn { get; } = Translation.Create(
        UnitConfiguration.Gram.Id,
        LanguageConfiguration.English.Id,
        Name.CreateSkipValidation("Gram"),
        Abbreviation.CreateSkipValidation("g"),
        new TranslationId(Guid.Parse("CA64BA08-93BB-432D-AFC2-869F6C543922"))
    );
    
    public static Translation GramPl { get; } = Translation.Create(
        UnitConfiguration.Gram.Id,
        LanguageConfiguration.Polish.Id,
        Name.CreateSkipValidation("Gram"),
        Abbreviation.CreateSkipValidation("g"),
        new TranslationId(Guid.Parse("0164A42A-A4B7-4E1F-8DB8-8158B4605DBC"))
    );
    
    public static Translation GramDe { get; } = Translation.Create(
        UnitConfiguration.Gram.Id,
        LanguageConfiguration.German.Id,
        Name.CreateSkipValidation("Gramm"),
        Abbreviation.CreateSkipValidation("g"),
        new TranslationId(Guid.Parse("B6101A50-6F20-42CF-B58D-1A69D77FFDFC"))
    );
    
    public static Translation MilliliterEn { get; } = Translation.Create(
        UnitConfiguration.Milliliter.Id,
        LanguageConfiguration.English.Id,
        Name.CreateSkipValidation("Milliliter"),
        Abbreviation.CreateSkipValidation("ml"),
        new TranslationId(Guid.Parse("3BF5D07A-6004-4F50-9BC4-59E8831F5E90"))
    );
    
    public static Translation MilliliterPl { get; } = Translation.Create(
        UnitConfiguration.Milliliter.Id,
        LanguageConfiguration.Polish.Id,
        Name.CreateSkipValidation("Mililitr"),
        Abbreviation.CreateSkipValidation("ml"),
        new TranslationId(Guid.Parse("67CEFC87-B43A-4262-9D16-9E12AAC5DB5E"))
    );
    
    public static Translation MilliliterDe { get; } = Translation.Create(
        UnitConfiguration.Milliliter.Id,
        LanguageConfiguration.German.Id,
        Name.CreateSkipValidation("Milliliter"),
        Abbreviation.CreateSkipValidation("ml"),
        new TranslationId(Guid.Parse("DC8020FA-C5C1-4E88-8658-752E5A922C3D"))
    );
    
    public static Translation PieceEn { get; } = Translation.Create(
        UnitConfiguration.Piece.Id,
        LanguageConfiguration.English.Id,
        Name.CreateSkipValidation("Piece"),
        Abbreviation.CreateSkipValidation("pc"),
        new TranslationId(Guid.Parse("C9194190-521F-40BA-836F-6A8DACCB418E"))
    );
    
    public static Translation PiecePl { get; } = Translation.Create(
        UnitConfiguration.Piece.Id,
        LanguageConfiguration.Polish.Id,
        Name.CreateSkipValidation("Sztuka"),
        Abbreviation.CreateSkipValidation("szt"),
        new TranslationId(Guid.Parse("256783E0-D6BA-4A6F-B0CC-52E3B908820C"))
    );
    
    public static Translation PieceDe { get; } = Translation.Create(
        UnitConfiguration.Piece.Id,
        LanguageConfiguration.German.Id,
        Name.CreateSkipValidation("Stück"),
        Abbreviation.CreateSkipValidation("st"),
        new TranslationId(Guid.Parse("831090A9-1736-4264-93FB-449AA3D3D02D"))
    );

    public void Configure(
        EntityTypeBuilder<Translation> builder
    )
    {
        builder.ToTable("unit_translations", "unit");
        builder.HasKey(translation => translation.Id);
        builder.Property(translation => translation.Id)
            .HasConversion(
                id => id.Value,
                value => value
            )
            .ValueGeneratedNever();

        builder.Property(translation => translation.UnitId)
            .HasConversion(
                unitId => unitId.Value,
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

        builder.Property(translation => translation.Abbreviation)
            .HasConversion(
                abbreviation => abbreviation.Value,
                value => Abbreviation.CreateSkipValidation(value)
            );

        builder.HasOne(translation => translation.Unit)
            .WithMany(unit => unit.Translations)
            .HasForeignKey(translation => translation.UnitId);

        builder.HasOne(translation => translation.Language)
            .WithMany()
            .HasForeignKey(translation => translation.LanguageId);

        builder.HasData(GetSeedData());
    }

    private static IEnumerable<Translation> GetSeedData()
    {
        yield return LiterEn;
        yield return LiterPl;
        yield return LiterDe;
        yield return KilogramEn;
        yield return KilogramPl;
        yield return KilogramDe;
        yield return GramEn;
        yield return GramPl;
        yield return GramDe;
        yield return MilliliterEn;
        yield return MilliliterPl;
        yield return MilliliterDe;
        yield return PieceEn;
        yield return PiecePl;
        yield return PieceDe;
    }
}