// # ==============================================================================
// # Solution: BiteRight
// # File: ProfileConfiguration.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

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

        builder.Property(profile => profile.TimeZone)
            .HasConversion(
                timeZone => timeZone.Id,
                value => TimeZoneInfo.FindSystemTimeZoneById(value)
            )
            .HasColumnName("time_zone_id");

        builder.Property(profile => profile.UserId)
            .HasConversion(
                userId => userId.Value,
                value => value
            );

        builder.HasOne(profile => profile.User)
            .WithOne(user => user.Profile)
            .HasForeignKey<Profile>(profile => profile.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}