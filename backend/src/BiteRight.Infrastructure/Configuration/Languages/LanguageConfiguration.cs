// # ==============================================================================
// # Solution: BiteRight
// # File: LanguageConfiguration.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using System.Collections.Generic;
using BiteRight.Domain.Languages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace BiteRight.Infrastructure.Configuration.Languages;

public class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public static Language Polish { get; } = Language.Create(
        Name.Create("Polski"),
        Name.Create("Polish"),
        Code.Create("pl"),
        new LanguageId(Guid.Parse("24D48691-7325-4703-B69F-8DB933A6736D"))
    );

    public static Language English { get; } = Language.Create(
        Name.Create("English"),
        Name.Create("English"),
        Code.Create("en"),
        new LanguageId(Guid.Parse("454FAF9A-644C-445C-89E3-B57203957C1A"))
    );

    public static Language German { get; } = Language.Create(
        Name.Create("Deutsch"),
        Name.Create("German"),
        Code.Create("de"),
        new LanguageId(Guid.Parse("C1DD0A3B-70D3-4AA1-B53E-4C08A03B57C3"))
    );

    public void Configure(
        EntityTypeBuilder<Language> builder
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
}