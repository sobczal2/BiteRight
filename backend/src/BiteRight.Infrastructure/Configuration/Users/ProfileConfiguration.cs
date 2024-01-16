using BiteRight.Domain.Countries;
using BiteRight.Domain.Currencies;
using BiteRight.Domain.Languages;
using BiteRight.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiteRight.Infrastructure.Configuration.Users;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(
        EntityTypeBuilder<Profile> builder
    )
    {
        builder.ToTable("profiles", "user");
        builder.HasKey(profile => profile.Id);
        builder.Property(profile => profile.Id)
            .HasConversion(
                id => id.Value,
                value => value
            )
            .ValueGeneratedNever();
        builder.Property(profile => profile.CountryId)
            .HasConversion(
                countryId => countryId.Value,
                value => value
            );
        builder.HasOne<Country>()
            .WithMany()
            .HasForeignKey(profile => profile.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(profile => profile.LanguageId)
            .HasConversion(
                languageId => languageId.Value,
                value => value
            );
        builder.HasOne<Language>()
            .WithMany()
            .HasForeignKey(profile => profile.LanguageId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(profile => profile.CurrencyId)
            .HasConversion(
                currencyId => currencyId.Value,
                value => value
            );
        builder.HasOne<Currency>()
            .WithMany()
            .HasForeignKey(profile => profile.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}