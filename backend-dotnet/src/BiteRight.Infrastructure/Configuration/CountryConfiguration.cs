using BiteRight.Domain.Countries;
using BiteRight.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Name = BiteRight.Domain.Countries.Name;

namespace BiteRight.Infrastructure.Configuration;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(
        EntityTypeBuilder<Country> builder
    )
    {
        builder.ToTable("countries", "country");
        builder.Ignore(country => country.DomainEvents);
        builder.HasKey(country => country.Id);
        builder.Property(country => country.Id)
            .HasConversion(
                id => id.Value,
                value => value
            )
            .ValueGeneratedNever();
        builder.Property(country => country.NativeName)
            .HasConversion(
                nativeName => nativeName.Value,
                value => Name.CreateSkipValidation(value)
            );
        builder.Property(country => country.EnglishName)
            .HasConversion(
                englishName => englishName.Value,
                value => Name.CreateSkipValidation(value)
            );
        builder.Property(country => country.Alpha2Code)
            .HasConversion(
                alpha2Code => alpha2Code.Value,
                value => Alpha2Code.CreateSkipValidation(value)
            );
        builder.Property(country => country.OfficialLanguageId)
            .HasConversion(
                officialLanguageId => officialLanguageId.Value,
                value => value
            );
        builder.HasData(GetSeedData());
    }

    private static IEnumerable<Country> GetSeedData()
    {
        yield return Poland;
        yield return England;
        yield return Germany;
        yield return Usa;
    }

    public static Country Poland { get; } = Country.Create(
        Name.Create("Polska"),
        Name.Create("Poland"),
        Alpha2Code.Create("pl"),
        LanguageConfiguration.Polish.Id,
        new SeedDomainEventFactory()
    );

    public static Country England { get; } = Country.Create(
        Name.Create("England"),
        Name.Create("England"),
        Alpha2Code.Create("en"),
        LanguageConfiguration.English.Id,
        new SeedDomainEventFactory()
    );

    public static Country Germany { get; } = Country.Create(
        Name.Create("Deutschland"),
        Name.Create("Germany"),
        Alpha2Code.Create("de"),
        LanguageConfiguration.German.Id,
        new SeedDomainEventFactory()
    );

    public static Country Usa { get; } = Country.Create(
        Name.Create("United States of America"),
        Name.Create("United States of America"),
        Alpha2Code.Create("us"),
        LanguageConfiguration.English.Id,
        new SeedDomainEventFactory()
    );
}