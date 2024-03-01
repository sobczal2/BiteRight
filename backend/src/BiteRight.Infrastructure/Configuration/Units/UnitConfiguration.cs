// # ==============================================================================
// # Solution: BiteRight
// # File: UnitConfiguration.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using System.Collections.Generic;
using BiteRight.Domain.Units;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace BiteRight.Infrastructure.Configuration.Units;

public class UnitConfiguration : IEntityTypeConfiguration<Unit>
{
    public static Unit Liter { get; } = Unit.Create(
        UnitSystem.Metric,
        new UnitId(Guid.Parse("B6D4D4DD-C035-4047-B8EE-48937CB1F368"))
    );

    public static Unit Kilogram { get; } = Unit.Create(
        UnitSystem.Metric,
        new UnitId(Guid.Parse("CDE52E6C-5D9D-4876-978A-BF67C02CC8BE"))
    );
    
    public static Unit Gram { get; } = Unit.Create(
        UnitSystem.Metric,
        new UnitId(Guid.Parse("84770558-3345-4D05-9814-21C61FBDBB09"))
    );
    
    public static Unit Milliliter { get; } = Unit.Create(
        UnitSystem.Metric,
        new UnitId(Guid.Parse("F13143DB-0380-4B90-8B8D-AAD8C9EA7D48"))
    );
    
    public static Unit Piece { get; } = Unit.Create(
        UnitSystem.Both,
        new UnitId(Guid.Parse("F8920B2A-D1EE-4351-A708-4B82584257F1"))
    );

    public void Configure(
        EntityTypeBuilder<Unit> builder
    )
    {
        builder.ToTable("units", "unit");
        builder.Ignore(unit => unit.DomainEvents);
        builder.HasKey(unit => unit.Id);
        builder.Property(unit => unit.Id)
            .HasConversion(
                id => id.Value,
                value => value
            )
            .ValueGeneratedNever();

        builder.HasData(GetSeedData());
    }

    private static IEnumerable<Unit> GetSeedData()
    {
        yield return Liter;
        yield return Kilogram;
        yield return Gram;
        yield return Milliliter;
        yield return Piece;
    }
}