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
        builder.Property(profile => profile.CurrencyId)
            .HasConversion(
                currencyId => currencyId.Value,
                value => value
            );
        builder.HasOne(profile => profile.Currency)
            .WithMany()
            .HasForeignKey(profile => profile.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}