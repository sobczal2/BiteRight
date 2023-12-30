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
        new SeedDomainEventFactory(),
        new CountryId(new Guid("35D08361-F753-4DB9-B88E-11C400D53EB7"))
    );

    public static Country England { get; } = Country.Create(
        Name.Create("England"),
        Name.Create("England"),
        Alpha2Code.Create("en"),
        LanguageConfiguration.English.Id,
        new SeedDomainEventFactory(),
        new CountryId(new Guid("F3E4C5CB-229C-4B2D-90DC-F83CB4A45F75"))
    );

    public static Country Germany { get; } = Country.Create(
        Name.Create("Deutschland"),
        Name.Create("Germany"),
        Alpha2Code.Create("de"),
        LanguageConfiguration.German.Id,
        new SeedDomainEventFactory(),
        new CountryId(new Guid("1352DE6E-C0BF-48C6-B703-FAE0B254D642"))
    );

    public static Country Usa { get; } = Country.Create(
        Name.Create("United States of America"),
        Name.Create("United States of America"),
        Alpha2Code.Create("us"),
        LanguageConfiguration.English.Id,
        new SeedDomainEventFactory(),
        new CountryId(new Guid("12E2937F-F04D-4150-A7AE-5AB1176A95D8"))
    );
}