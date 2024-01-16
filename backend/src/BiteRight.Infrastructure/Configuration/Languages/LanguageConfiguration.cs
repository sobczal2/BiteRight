using BiteRight.Domain.Languages;
using BiteRight.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiteRight.Infrastructure.Configuration.Languages;

public class LanguageConfiguration : IEntityTypeConfiguration<BiteRight.Domain.Languages.Language>
{
    public void Configure(
        EntityTypeBuilder<BiteRight.Domain.Languages.Language> builder
    )
    {
        builder.ToTable("languages", "language");
        builder.Ignore(language => language.DomainEvents);
        builder.HasKey(language => language.Id);
        builder.Property(language => language.Id)
            .HasConversion(
                id => id.Value,
                value => value
            )
            .ValueGeneratedNever();
        builder.Property(language => language.Code)
            .HasConversion(
                code => code.Value,
                value => Code.CreateSkipValidation(value)
            );
        builder.Property(language => language.NativeName)
            .HasConversion(
                name => name.Value,
                value => Name.CreateSkipValidation(value)
            );
        builder.Property(language => language.EnglishName)
            .HasConversion(
                name => name.Value,
                value => Name.CreateSkipValidation(value)
            );

        builder.HasData(GetSeedData());
    }

    private static IEnumerable<Language> GetSeedData()
    {
        yield return Polish;
        yield return English;
        yield return German;
    }

    public static BiteRight.Domain.Languages.Language Polish { get; } = BiteRight.Domain.Languages.Language.Create(
        Name.Create("Polski"),
        Name.Create("Polish"),
        Code.Create("pl"),
        new SeedDomainEventFactory(),
        new LanguageId(new Guid("24D48691-7325-4703-B69F-8DB933A6736D"))
    );

    public static BiteRight.Domain.Languages.Language English { get; } = BiteRight.Domain.Languages.Language.Create(
        Name.Create("English"),
        Name.Create("English"),
        Code.Create("en"),
        new SeedDomainEventFactory(),
        new LanguageId(new Guid("454FAF9A-644C-445C-89E3-B57203957C1A"))
    );

    public static BiteRight.Domain.Languages.Language German { get; } = BiteRight.Domain.Languages.Language.Create(
        Name.Create("Deutsch"),
        Name.Create("German"),
        Code.Create("de"),
        new SeedDomainEventFactory(),
        new LanguageId(new Guid("C1DD0A3B-70D3-4AA1-B53E-4C08A03B57C3"))
    );
}