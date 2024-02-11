using BiteRight.Domain.Units;
using BiteRight.Infrastructure.Configuration.Languages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Name = BiteRight.Domain.Units.Name;

namespace BiteRight.Infrastructure.Configuration.Units;

public class TranslationConfiguration : IEntityTypeConfiguration<Translation>
{
    public static Translation LiterEn { get; } = Translation.Create(
        UnitConfiguration.Liter.Id,
        LanguageConfiguration.English.Id,
        Name.CreateSkipValidation("Liter"),
        Abbreviation.CreateSkipValidation("L")
    );

    public static Translation LiterPl { get; } = Translation.Create(
        UnitConfiguration.Liter.Id,
        LanguageConfiguration.Polish.Id,
        Name.CreateSkipValidation("Litr"),
        Abbreviation.CreateSkipValidation("L")
    );

    public static Translation LiterDe { get; } = Translation.Create(
        UnitConfiguration.Liter.Id,
        LanguageConfiguration.German.Id,
        Name.CreateSkipValidation("Liter"),
        Abbreviation.CreateSkipValidation("L")
    );

    public static Translation KilogramEn { get; } = Translation.Create(
        UnitConfiguration.Kilogram.Id,
        LanguageConfiguration.English.Id,
        Name.CreateSkipValidation("Kilogram"),
        Abbreviation.CreateSkipValidation("kg")
    );

    public static Translation KilogramPl { get; } = Translation.Create(
        UnitConfiguration.Kilogram.Id,
        LanguageConfiguration.Polish.Id,
        Name.CreateSkipValidation("Kilogram"),
        Abbreviation.CreateSkipValidation("kg")
    );

    public static Translation KilogramDe { get; } = Translation.Create(
        UnitConfiguration.Kilogram.Id,
        LanguageConfiguration.German.Id,
        Name.CreateSkipValidation("Kilogramm"),
        Abbreviation.CreateSkipValidation("kg")
    );

    public void Configure(EntityTypeBuilder<Translation> builder)
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
    }
}